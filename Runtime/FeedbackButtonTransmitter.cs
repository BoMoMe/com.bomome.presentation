using UnityEngine;
using UnityEngine.UI;

namespace Bomome.UI.Feedbacks
{
    [UnityEngine.Scripting.APIUpdating.MovedFrom(true, sourceNamespace: "Bomome.UI.Feedbacks", sourceAssembly: "Bomome")]
    [DisallowMultipleComponent, RequireComponent(typeof(Button))]
    public sealed class FeedbackButtonTransmitter : MonoBehaviour
    {
        [SerializeField] private FeedbackChannel channel;
        [SerializeField] private bool checkInteractable = true;
        private Button button;

        private void Awake() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(Transmit);
        private void OnDisable() => button.onClick.RemoveListener(Transmit);

        public void Transmit()
        {
            if (!checkInteractable || (isActiveAndEnabled && button.IsInteractable())) channel?.Raise(gameObject);
        }
    }
}
