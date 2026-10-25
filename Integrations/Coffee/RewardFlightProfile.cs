using UnityEngine;

namespace Bomome.Presentation.RewardFlights
{
    [CreateAssetMenu(menuName = "Bomome/Presentation/Reward Flight Profile", fileName = "RewardFlightProfile")]
    public sealed class RewardFlightProfile : ScriptableObject
    {
        [Header("Presentation")]
        [Tooltip("A dedicated Coffee effect prefab. Art, burst, lifetime and attraction are authored on this prefab.")]
        [SerializeField] private CoffeeRewardFlightEffect effectPrefab;
        [Min(1), Tooltip("Icon count when the caller omits a count. Not the actual granted reward amount.")]
        [SerializeField] private int defaultVisualCount = 10;
        [Min(1), Tooltip("Maximum icons per request; the presenter's total budget can reduce this further.")]
        [SerializeField] private int maximumVisualCount = 32;
        [Min(0.1f), Tooltip("Unscaled seconds after emission before cleanup. Set above particle lifetime.")]
        [SerializeField] private float timeoutSeconds = 6f;

        public CoffeeRewardFlightEffect EffectPrefab => effectPrefab;
        public int DefaultVisualCount => Mathf.Max(1, defaultVisualCount);
        public int MaximumVisualCount => Mathf.Max(1, maximumVisualCount);
        public float TimeoutSeconds => timeoutSeconds;
    }
}
