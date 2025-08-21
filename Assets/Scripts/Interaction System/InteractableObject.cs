using Misc;
using Progress_System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InteractionSystem
{
    [RequireComponent(typeof(SpriteRenderer),typeof(EventTrigger))]
    public class InteractableObject : MonoBehaviour
    {
        private SpriteRenderer _xSpriteRenderer;
        private EventTrigger _xEventTrigger;
        private Condition _xPreConditions;
        private Condition _xPostConditions;
        public Condition xPreConditions { get => _xPreConditions; }


        private void OnEnable()
        {
            GameManager.Instance.XDialogueEventBus.Register(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION,CheckPreConditions);
            CheckPreConditions(null);
        }

        private void OnDisable()
        {
            GameManager.Instance.XDialogueEventBus.Unregister(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION, CheckPreConditions);
        }

        public void ApplayPostConditions()
        {
            ConditionsUtils.ApplyCondition(_xPostConditions);
            GameManager.Instance.XDialogueEventBus.TriggerEvent(InteractEventList.ON_CONDITION_CHANGE);
        }

        public void CheckPreConditions(params object[] param)
        {
            // if the preconditions are not met deactivate Image and EventTrigger
            if (!ConditionsUtils.CheckConditions(_xPreConditions))
            {
                _xSpriteRenderer.enabled = false;
                _xEventTrigger.enabled = false;
                return;
            }

            _xSpriteRenderer.enabled = true;
            _xEventTrigger.enabled = true;
        }
    }
}

