using Misc;
using UnityEngine;

namespace InteractionSystem
{
    public class InteractableManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.XDialogueEventBus.Register(InteractEventList.ON_CONDITION_CHANGE, RefreshInteractables);
        }

        private void OnDisable()
        {
            GameManager.Instance.XDialogueEventBus.Unregister(InteractEventList.ON_CONDITION_CHANGE, RefreshInteractables);
        }

        void RefreshInteractables(params object[] param) => GameManager.Instance.XDialogueEventBus.TriggerEvent(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION);
    }
}

