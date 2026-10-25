using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bomome.Presentation.RewardFlights
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class RewardFlightTarget : MonoBehaviour
    {
        [Tooltip("Invoked per icon arrival. Connect a Feel player here; throttle accumulating SFX in your audio policy.")]
        [SerializeField] private UnityEvent onCollected = new UnityEvent();
        [Tooltip("Invoked only when every icon in one flight reached this target, not on expiration or cancellation.")]
        [SerializeField] private UnityEvent onFlightCollected = new UnityEvent();
        public RectTransform Anchor => (RectTransform)transform;
        internal void NotifyCollected() => InvokeSafely(onCollected);
        internal void NotifyFlightCollected() => InvokeSafely(onFlightCollected);
        internal static void InvokeSafely(UnityEvent feedback)
        {
            try { feedback?.Invoke(); }
            catch (Exception exception) { Debug.LogException(exception); }
        }
    }
}
