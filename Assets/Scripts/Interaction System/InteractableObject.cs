using Misc;
using Progress_System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

namespace InteractionSystem
{
    [RequireComponent(typeof(SpriteRenderer), typeof(EventTrigger), typeof(BoxCollider2D))]
    public class InteractableObject : MonoBehaviour
    {

        [SerializeField] private Condition _xPreConditions;
        [SerializeField] private Condition _xPostConditions;

        private SpriteRenderer _xSpriteRenderer;
        private EventTrigger _xEventTrigger;
        private Entry _xEntry = new();
        public Condition xPreConditions { get => _xPreConditions; }


        private void OnEnable()
        {
            _xEventTrigger = GetComponent<EventTrigger>();
            _xSpriteRenderer = GetComponent<SpriteRenderer>();

            // start listening for the EventTrigger's OnClick
            _xEntry.eventID = EventTriggerType.PointerClick;
            _xEntry.callback.AddListener((eventData) => { ApplayPostConditions(); });
            _xEventTrigger.triggers.Add(_xEntry);

            CheckPreConditions();

            GameManager.Instance.XInteractableEventBus.Register(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION, CheckPreConditions);
        }


        private void OnDisable()
        {
            _xEventTrigger.triggers.Remove(_xEntry);
            GameManager.Instance.XInteractableEventBus.Unregister(InteractEventList.REFRESH_INTERACTABLE_PRE_CONDITION, CheckPreConditions);
        }

        /// <summary>
        /// applaies the conditions and then triggers the ON_CONDITION_CHANGE event 
        /// </summary>
        private void ApplayPostConditions()
        {

            ConditionsUtils.ApplyCondition(_xPostConditions);
            GameManager.Instance.XInteractableEventBus.TriggerEvent(InteractEventList.ON_CONDITION_CHANGE); // calls the diary to refresh the ui
        }

        /// <summary>
        /// checks if the item should be visible by the player
        /// </summary>
        /// <param name="param">no need to put anything here</param>
        private void CheckPreConditions(params object[] param)
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

