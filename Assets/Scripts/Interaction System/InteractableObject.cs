using Misc;
using Progress_System;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InteractionSystem
{
    [RequireComponent(typeof(SpriteRenderer),typeof(EventTrigger))]
    public class InteractableObject : MonoBehaviour
    {

        [SerializeField] private Condition _xPreConditions;
        [SerializeField] private Condition _xPostConditions;
      
        private SpriteRenderer _xSpriteRenderer;
        private EventTrigger _xEventTrigger;
        public Condition xPreConditions { get => _xPreConditions; }


        private void OnEnable()
        {
            _xEventTrigger = GetComponent<EventTrigger>();
            _xSpriteRenderer = GetComponent<SpriteRenderer>();

            // start listening for the EventTrigger
            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { ApplayPostConditions(); });
            _xEventTrigger.triggers.Add(entry);

            CheckPreConditions();
            
            GameManager.Instance.XDialogueEventBus.Register(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION,CheckPreConditions);
        }


        private void OnDisable()
        {
            GameManager.Instance.XDialogueEventBus.Unregister(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION, CheckPreConditions);
        }

        public void ApplayPostConditions()
        {
            ConditionsUtils.ApplyCondition(_xPostConditions);
            GameManager.Instance.XDialogueEventBus.TriggerEvent(InteractEventList.ON_CONDITION_CHANGE); // calls the diary to check if it's an item and all other interactables to check their pre conditions
        }

        public void CheckPreConditions(params object[] param)
        {
            // if the preconditions are not met deactivate 
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

