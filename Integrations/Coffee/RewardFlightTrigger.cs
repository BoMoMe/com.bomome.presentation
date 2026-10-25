using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bomome.Presentation.RewardFlights
{
    /// <summary>Inspector-friendly adapter for Button.onClick or any UnityEvent.</summary>
    public sealed class RewardFlightTrigger : MonoBehaviour
    {
        [Serializable]
        private sealed class Entry
        {
            public RewardFlightProfile Profile;
            public RewardFlightTarget Target;
            [Min(1)] public int VisualCount = 10;
            [Min(0)] public float DelaySeconds;
        }

        [Header("References")]
        [SerializeField] private RewardFlightPresenter presenter;
        [SerializeField] private RectTransform source;
        [SerializeField] private Entry[] flights = Array.Empty<Entry>();
        [Header("Feedbacks")]
        [Tooltip("Every flight has settled, including interrupted or rejected flights. Does not imply currency was granted.")]
        [SerializeField] private UnityEvent onFinished = new UnityEvent();
        [Tooltip("Every icon in this batch reached its target.")]
        [SerializeField] private UnityEvent onAllCollected = new UnityEvent();
        private readonly List<RewardFlightBatch> owned = new List<RewardFlightBatch>();
        private bool cancelling;
        public RewardFlightBatch LastBatch { get; private set; }

        public void Play()
        {
            if (!isActiveAndEnabled || cancelling || presenter == null || source == null) return;
            var requests = new RewardFlightRequest[flights.Length];
            Vector2 screen = RewardFlightPresenter.ScreenPosition(source);
            for (int i = 0; i < flights.Length; i++)
            {
                var entry = flights[i];
                requests[i] = new RewardFlightRequest(entry.Profile, screen, entry.Target,
                    entry.VisualCount, entry.DelaySeconds);
            }
            LastBatch = presenter.PlayBatch(requests);
            if (LastBatch.IsDone) OnFinished(LastBatch);
            else { owned.Add(LastBatch); LastBatch.Finished += OnFinished; }
        }

        public void Cancel()
        {
            if (cancelling) return;
            cancelling = true;
            try { while (owned.Count > 0) owned[owned.Count - 1].Cancel(); }
            finally { cancelling = false; }
        }

        private void OnDisable() => Cancel();
        private void OnFinished(RewardFlightBatch batch)
        {
            batch.Finished -= OnFinished;
            owned.Remove(batch);
            RewardFlightTarget.InvokeSafely(onFinished);
            if (batch.AllCollected) RewardFlightTarget.InvokeSafely(onAllCollected);
        }
    }
}
