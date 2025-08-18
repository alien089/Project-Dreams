using Progress_System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InteractionSystem
{
    public interface IObjectInteractable
    {
        Image _iImage { get; set; }
        EventTrigger _xEventTrigger { get; set; }
        Condition _xPreConditions { get; set; }
        Condition _xPostConditions { get; set; }

        void CheckPreConditions(object[] param);
        void ApplayPostConditions();


    }
}
