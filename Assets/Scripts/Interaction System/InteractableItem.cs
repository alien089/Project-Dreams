using Progress_System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InteractionSystem
{
    public class InteractableItem : IObjectInteractable
    {
        public Image _iImage { get; set; }
        public EventTrigger _xEventTrigger { get; set; }
        public Condition _xPreConditions { get; set; }
        public Condition _xPostConditions { get; set; }

        public void ApplayPostConditions()
        {
            
        }

        public void CheckPreConditions(object[] param)
        {
        }
    }
}

