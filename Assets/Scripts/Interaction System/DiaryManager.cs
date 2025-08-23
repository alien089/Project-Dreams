using Misc;
using Progress_System;
using System.Collections.Generic;
using UnityEngine;


namespace InteractionSystem
{

    public class DiaryManager : MonoBehaviour
    {
        [SerializeField] private ConditionItemMap _xItemMap;

        // testing
        [SerializeField] private GameObject canvas;
        [SerializeField] private UnityEngine.UI.Image itemIcon;
        [SerializeField] private UnityEngine.UI.Image itemImg;
        [SerializeField] private TMPro.TextMeshProUGUI itemName;
        [SerializeField] private TMPro.TextMeshProUGUI itemDescription;

        private void OnEnable()
        {
            GameManager.Instance.XInteractableEventBus.Register(InteractEventList.ON_CONDITION_CHANGE,OnItemListChanged);
        }

        private void OnDisable()
        {
            GameManager.Instance.XInteractableEventBus.Unregister(InteractEventList.ON_CONDITION_CHANGE, OnItemListChanged);
        }

        public void OnItemListChanged(params object[] param)
        {
            // get the conditions the player has and then check if any of them are items and then change the diary UI
            ActualDialogueCondition actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse")[0];

            
            foreach (KeyValuePair<Conditions, int> pair in actualConditions.MConditions)
            {
                if (!_xItemMap.TryGetValue(pair.Key, out ItemDataSO itemData))
                    continue;

                if (!itemData.xObjectConditonalData.TryGetValue(pair.Value, out ItemModel itemModel))
                    return;

                // Change the diary UI:

                // get a int and every time you get here increment it 
                // use this value to place the items in the diary

                // testing
                
                itemIcon.sprite = itemModel.xObjectIcon;
                itemImg.sprite = itemModel.xObjectImage;
                itemName.text = itemModel.sName;
                itemDescription.text = itemModel.sDescription;


            }

        }

        // TESTING TO REMOVE
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(canvas.activeInHierarchy)
                    canvas.SetActive(false);
               else
                    canvas.SetActive(true);
            }
        }

    }
}
