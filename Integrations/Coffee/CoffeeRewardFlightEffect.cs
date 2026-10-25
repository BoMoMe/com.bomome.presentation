using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Bomome.Presentation.RewardFlights
{
    /// <summary>One isolated particle stream and attractor per pooled flight.</summary>
    public sealed class CoffeeRewardFlightEffect : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UIParticle uiParticle;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private UIParticleAttractor attractor;
        [Header("Feedbacks")]
        [SerializeField] private UnityEvent onBurst = new UnityEvent();
        [SerializeField] private UnityEvent onCollected = new UnityEvent();
        private int arrivals;
        public bool IsConfigured => uiParticle != null && particles != null && attractor != null;
        public bool HasParticles => particles != null && particles.particleCount > 0;

        internal void Begin(int count, Vector3 destination)
        {
            arrivals = 0;
            uiParticle.Stop();
            uiParticle.Clear();
            var main = particles.main;
            main.loop = false;
            main.playOnAwake = false;
            main.useUnscaledTime = true;
            main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;
            main.maxParticles = Mathf.Max(main.maxParticles, count);
            var emission = particles.emission;
            emission.enabled = false;
            attractor.transform.position = destination;
            attractor.updateMode = UIParticleAttractor.UpdateMode.UnscaledTime;
            attractor.AddParticleSystem(particles);
            if (attractor.onAttracted == null) attractor.onAttracted = new UnityEvent();
            attractor.onAttracted.RemoveListener(OnAttracted);
            attractor.onAttracted.AddListener(OnAttracted);
            uiParticle.Play();
            particles.Emit(count);
        }

        internal void BurstFeedback() => RewardFlightTarget.InvokeSafely(onBurst);
        internal void CollectionFeedback() => RewardFlightTarget.InvokeSafely(onCollected);
        internal void SetDestination(Vector3 destination) => attractor.transform.position = destination;
        internal int TakeArrivals() { int count = arrivals; arrivals = 0; return count; }

        internal void ResetForPool()
        {
            if (attractor != null && attractor.onAttracted != null)
                attractor.onAttracted.RemoveListener(OnAttracted);
            if (uiParticle != null) { uiParticle.Stop(); uiParticle.Clear(); }
            arrivals = 0;
        }

        // Coffee invokes inside its particle loop. Defer user feedback until the presenter tick.
        private void OnAttracted() => arrivals++;
        private void OnDisable() => ResetForPool();
    }
}
