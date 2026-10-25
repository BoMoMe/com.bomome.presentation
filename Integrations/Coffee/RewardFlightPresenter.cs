using System;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Bomome.Presentation.RewardFlights
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class RewardFlightPresenter : MonoBehaviour
    {
        [Header("Visual Budget")]
        [Min(1), SerializeField, Tooltip("Includes flights waiting for a delayed start. Overflow is rejected, never recycled mid-flight.")]
        private int maximumConcurrentFlights = 8;
        [Min(1), SerializeField, Tooltip("Maximum reserved icons across all active and delayed flights.")]
        private int maximumParticles = 128;

        private sealed class Flight
        {
            public RewardFlightRequest Request;
            public RewardFlightHandle Handle;
            public CoffeeRewardFlightEffect Effect;
            public double StartAt;
            public double EmittedAt;
            public bool Started;
        }

        private readonly List<Flight> active = new List<Flight>();
        private readonly List<Flight> tickBuffer = new List<Flight>();
        private RectTransform layer;
        private Canvas canvas;
        private int reservedParticles;
        private bool cancellingAll;
        public int ActiveFlightCount => active.Count;
        public int ReservedParticleCount => reservedParticles;

        private void Awake()
        {
            layer = (RectTransform)transform;
            canvas = GetComponentInParent<Canvas>();
        }

        public RewardFlightHandle Play(RewardFlightProfile profile, RectTransform source,
            RewardFlightTarget target, int count = -1)
        {
            if (source == null) return Rejected();
            return Play(new RewardFlightRequest(profile, ScreenPosition(source), target, count));
        }

        public RewardFlightHandle Play(RewardFlightRequest request)
        {
            var profile = request.Profile;
            if (!isActiveAndEnabled || cancellingAll || canvas == null || profile == null ||
                profile.EffectPrefab == null || !profile.EffectPrefab.IsConfigured ||
                request.Target == null || !request.Target.isActiveAndEnabled ||
                !Finite(request.SourceScreenPosition.x) || !Finite(request.SourceScreenPosition.y) ||
                !Finite(request.DelaySeconds) || request.DelaySeconds < 0 ||
                !Finite(profile.TimeoutSeconds) || profile.TimeoutSeconds <= 0 ||
                active.Count >= maximumConcurrentFlights) return Rejected();

            int desired = request.VisualCount == -1 ? profile.DefaultVisualCount : request.VisualCount;
            int count = Mathf.Min(desired, profile.MaximumVisualCount, maximumParticles - reservedParticles);
            if (count <= 0) return Rejected();
            var handle = new RewardFlightHandle(count);
            var flight = new Flight { Request = request, Handle = handle,
                StartAt = Time.unscaledTimeAsDouble + request.DelaySeconds };
            handle.BindCancellation(() => Finish(flight, RewardFlightStatus.Cancelled));
            active.Add(flight);
            reservedParticles += count;
            return handle;
        }

        public RewardFlightBatch PlayBatch(IReadOnlyList<RewardFlightRequest> requests,
            Action<RewardFlightBatch> onComplete = null)
        {
            if (requests == null) throw new ArgumentNullException(nameof(requests));
            var batch = new RewardFlightBatch();
            if (onComplete != null) batch.Finished += onComplete;
            for (int i = 0; i < requests.Count; i++) batch.Add(Play(requests[i]));
            batch.Seal();
            return batch;
        }

        public void CancelAll()
        {
            if (cancellingAll) return;
            cancellingAll = true;
            try { while (active.Count > 0) Finish(active[active.Count - 1], RewardFlightStatus.Cancelled); }
            finally { cancellingAll = false; }
        }

        private void OnDisable() => CancelAll();

        private void Update()
        {
            tickBuffer.Clear();
            tickBuffer.AddRange(active);
            double now = Time.unscaledTimeAsDouble;
            foreach (var flight in tickBuffer)
            {
                if (flight.Handle.IsDone) continue;
                var target = flight.Request.Target;
                if (target == null || !target.isActiveAndEnabled)
                { Finish(flight, RewardFlightStatus.TargetLost); continue; }
                if (now < flight.StartAt) continue;
                if (!TryWorldPosition(ScreenPosition(target.Anchor), out Vector3 destination))
                { Finish(flight, RewardFlightStatus.TargetLost); continue; }
                if (flight.Effect == null)
                {
                    if (flight.Started) { Finish(flight, RewardFlightStatus.Cancelled); continue; }
                    if (!TryWorldPosition(flight.Request.SourceScreenPosition, out Vector3 source))
                    { Finish(flight, RewardFlightStatus.Rejected); continue; }
                    flight.Effect = LeanPool.Spawn(flight.Request.Profile.EffectPrefab, transform, false);
                    if (flight.Effect == null) { Finish(flight, RewardFlightStatus.Rejected); continue; }
                    flight.Effect.transform.position = source;
                    flight.Effect.Begin(flight.Handle.VisualCount, destination);
                    flight.EmittedAt = now;
                    flight.Started = true;
                    flight.Effect.BurstFeedback();
                    continue;
                }
                flight.Effect.SetDestination(destination);
                int arrivals = flight.Effect.TakeArrivals();
                for (int i = 0; i < arrivals && !flight.Handle.IsDone; i++)
                {
                    flight.Handle.Collect();
                    flight.Effect.CollectionFeedback();
                    if (!flight.Handle.IsDone && target != null) target.NotifyCollected();
                }
                if (flight.Handle.IsDone) continue;
                if (flight.Handle.CollectedCount >= flight.Handle.VisualCount)
                    Finish(flight, RewardFlightStatus.Completed);
                else if (now - flight.EmittedAt >= flight.Request.Profile.TimeoutSeconds || !flight.Effect.HasParticles)
                    Finish(flight, RewardFlightStatus.Expired);
            }
            tickBuffer.Clear();
        }

        private void Finish(Flight flight, RewardFlightStatus status)
        {
            if (flight.Handle.IsDone || !active.Remove(flight)) return;
            reservedParticles -= flight.Handle.VisualCount;
            if (flight.Effect != null)
            {
                flight.Effect.ResetForPool();
                LeanPool.Despawn(flight.Effect);
                flight.Effect = null;
            }
            // Cleanup is complete before any user callback can launch another flight.
            flight.Handle.Finish(status);
            if (status == RewardFlightStatus.Completed && flight.Request.Target != null)
                flight.Request.Target.NotifyFlightCollected();
        }

        public static Vector2 ScreenPosition(RectTransform rect)
        {
            var sourceCanvas = rect.GetComponentInParent<Canvas>();
            Camera camera = sourceCanvas != null && sourceCanvas.rootCanvas.renderMode != RenderMode.ScreenSpaceOverlay
                ? sourceCanvas.rootCanvas.worldCamera : null;
            return RectTransformUtility.WorldToScreenPoint(camera, rect.position);
        }

        private bool TryWorldPosition(Vector2 screen, out Vector3 position)
        {
            if (canvas == null) { position = default; return false; }
            var root = canvas.rootCanvas;
            if (root.renderMode != RenderMode.ScreenSpaceOverlay && root.worldCamera == null)
            { position = default; return false; }
            var camera = root.renderMode == RenderMode.ScreenSpaceOverlay ? null : root.worldCamera;
            return RectTransformUtility.ScreenPointToWorldPointInRectangle(layer, screen, camera, out position);
        }

        private static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
        private static RewardFlightHandle Rejected()
        {
            var handle = new RewardFlightHandle(0);
            handle.Finish(RewardFlightStatus.Rejected);
            return handle;
        }
    }
}
