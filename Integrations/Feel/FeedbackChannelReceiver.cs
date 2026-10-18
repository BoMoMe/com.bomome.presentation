using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Bomome.UI.Feedbacks
{
    [UnityEngine.Scripting.APIUpdating.MovedFrom(true, sourceNamespace: "Bomome.UI.Feedbacks", sourceAssembly: "Bomome")]
    [DisallowMultipleComponent]
    public sealed class FeedbackChannelReceiver : MonoBehaviour
    {
        [Tooltip("Central channel-to-feedback library. Each channel asset is the identifier; configure clips on its Feel player.")]
        [SerializeField] private Entry[] feedbacks = Array.Empty<Entry>();
        private readonly Dictionary<FeedbackChannel, Entry> lookup = new Dictionary<FeedbackChannel, Entry>();

        private void OnEnable()
        {
            RebuildCache();
        }

        private void OnDisable()
        {
            ClearCache();
        }

        private void ClearCache()
        {
            foreach (FeedbackChannel channel in lookup.Keys)
                if (channel != null) channel.Unsubscribe(Play);
            lookup.Clear();
        }

        [ContextMenu("Rebuild Feedback Cache")]
        public void RebuildCache()
        {
            ClearCache();
            foreach (Entry entry in feedbacks)
            {
                if (entry == null || entry.Channel == null) continue;
                if (lookup.ContainsKey(entry.Channel))
                {
                    Debug.LogWarning($"Duplicate feedback channel '{entry.Channel.name}'. First entry is used.", this);
                    continue;
                }
                entry.NextAllowedTime = 0f;
                lookup.Add(entry.Channel, entry);
                if (isActiveAndEnabled) entry.Channel.Subscribe(Play);
            }
        }

        private void Play(FeedbackChannel channel, GameObject sender)
        {
            if (!isActiveAndEnabled) return;
            if (!lookup.TryGetValue(channel, out Entry entry) || entry.Feedback == null)
            {
                channel.Trace("No feedback assigned", gameObject);
                return;
            }
            if (Time.unscaledTime < entry.NextAllowedTime) return;
            entry.NextAllowedTime = Time.unscaledTime + Mathf.Max(0f, entry.MinimumIntervalSeconds);
            channel.Trace($"Received; playing {entry.Feedback.name}", gameObject);
            entry.Feedback.PlayFeedbacks(transform.position);
        }

        [Serializable]
        private sealed class Entry
        {
            public FeedbackChannel Channel;
            public MMF_Player Feedback;
            [Min(0f)] public float MinimumIntervalSeconds;
            [NonSerialized] public float NextAllowedTime;
        }
    }
}
