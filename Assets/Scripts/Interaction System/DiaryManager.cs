using Misc;
using Progress_System;
using System.Collections.Generic;
using UnityEngine;


namespace InteractionSystem
{
    public class DiaryManager : MonoBehaviour
    {
        [SerializeField] private ConditionItemMap _xItemMap;

#if UNITY_EDITOR
        public ConditionItemMap map { get => _xItemMap; set => _xItemMap = value; }
#endif

        private void OnEnable()
        {
            GameManager.Instance.XInteractableEventBus.Register(InteractEventList.ON_CONDITION_CHANGE, OnItemListChanged);
        }

        private void OnDisable()
        {
            GameManager.Instance.XInteractableEventBus.Unregister(InteractEventList.ON_CONDITION_CHANGE, OnItemListChanged);
        }

        public void OnItemListChanged(params object[] param)
        {
            // get the conditions the player has and then check if any of them are items and then change the diary UI
            ActualDialogueCondition actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse")[0];

            List<DiaryUiItemData> itemsChanged = new();

            foreach (KeyValuePair<Conditions, int> pair in actualConditions.MConditions)
            {
                if (!_xItemMap.TryGetValue(pair.Key, out ItemDataSO itemData))
                    continue;

                if (!itemData.xObjectConditonalData.TryGetValue(pair.Value, out ItemModel itemModel))
                    return;

                // add to the changed items
                itemsChanged.Add(new(pair.Key, itemModel, itemData.xObjectType));
            }

            // Change the diary UI:
            GameManager.Instance.XInteractableEventBus.TriggerEvent(InteractEventList.ON_DIARY_CHANGE, itemsChanged);
        }
    }
}
