using Misc;
using Progress_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InteractionSystem
{

    public class DiaryManager : MonoBehaviour
    {
        [SerializeField] private ConditionItemMap _xItemMap;

        private void OnEnable()
        {
            GameManager.Instance.XDialogueEventBus.Register(InteractEventList.ON_CONDITION_CHANGE,OnItemListChanged);
        }

        private void OnDisable()
        {
            GameManager.Instance.XDialogueEventBus.Unregister(InteractEventList.ON_CONDITION_CHANGE, OnItemListChanged);
        }

        public void OnItemListChanged(params object[] param)
        {
            //ActualDialogueCondition actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse")[0];

            //foreach (KeyValuePair<Conditions, int> pair in actualConditions.MConditions)
            //{
            //    if (!_xItemMap.TryGetValue(pair.Key, out var objectModel))
            //        continue;

                

            //}
            
        }

    }
}
