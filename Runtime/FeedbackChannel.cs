using System;
using UnityEngine;

namespace Bomome.UI.Feedbacks
{
    [UnityEngine.Scripting.APIUpdating.MovedFrom(true, sourceNamespace: "Bomome.UI.Feedbacks", sourceAssembly: "Bomome")]
    [CreateAssetMenu(menuName = "Bomome/Feedbacks/Channel", fileName = "FeedbackChannel")]
    public sealed class FeedbackChannel : ScriptableObject
    {
        [TextArea] [SerializeField] private string description;
        [Tooltip("Trace sender and receiver hierarchy paths in Editor/development builds only.")]
        [SerializeField] private bool debugLogging;
        private event Action<FeedbackChannel, GameObject> listeners;
        private bool dispatching;

        public void Subscribe(Action<FeedbackChannel, GameObject> listener) => listeners += listener;
        public void Unsubscribe(Action<FeedbackChannel, GameObject> listener) => listeners -= listener;

        public void Raise(GameObject sender)
        {
            if (dispatching)
            {
                Trace("Ignored recursive trigger", sender);
                return;
            }
            Trace(listeners == null ? "Triggered (no active receiver)" : "Triggered", sender);
            dispatching = true;
            try { listeners?.Invoke(this, sender); }
            finally { dispatching = false; }
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
        public void Trace(string action, GameObject target)
        {
            if (!debugLogging) return;
            string path = target != null ? target.name : "<none>";
            for (Transform parent = target != null ? target.transform.parent : null; parent != null; parent = parent.parent)
                path = parent.name + "/" + path;
            string scene = target != null ? target.scene.name : "";
            Debug.Log($"[FeedbackChannel:{name}] {action}: {scene}/{path}", target != null ? (UnityEngine.Object)target : this);
        }
    }
}
