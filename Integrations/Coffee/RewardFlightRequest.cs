using UnityEngine;

namespace Bomome.Presentation.RewardFlights
{
    public readonly struct RewardFlightRequest
    {
        public readonly RewardFlightProfile Profile;
        public readonly Vector2 SourceScreenPosition;
        public readonly RewardFlightTarget Target;
        public readonly int VisualCount;
        public readonly float DelaySeconds;

        public RewardFlightRequest(RewardFlightProfile profile, Vector2 sourceScreenPosition,
            RewardFlightTarget target, int visualCount = -1, float delaySeconds = 0f)
        {
            Profile = profile;
            SourceScreenPosition = sourceScreenPosition;
            Target = target;
            VisualCount = visualCount;
            DelaySeconds = delaySeconds;
        }
    }
}
