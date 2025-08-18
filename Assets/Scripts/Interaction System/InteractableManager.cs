using Misc;
using UnityEngine;

namespace InteractionSystem
{
    public class InteractableManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.XDialogueEventBus.Register(InteractEventList.ADD_ITEM, RefreshInteractables);
        }

        private void OnDisable()
        {
            GameManager.Instance.XDialogueEventBus.Unregister(InteractEventList.ADD_ITEM, RefreshInteractables);
        }

        void RefreshInteractables(object[] param) => GameManager.Instance.XDialogueEventBus.TriggerEvent(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION);
    }
}

