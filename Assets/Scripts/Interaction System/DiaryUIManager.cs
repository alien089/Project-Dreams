using InteractionSystem;
using Misc;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUIManager : MonoBehaviour
{
    [SerializeField] private GameObject DiaryUiPannel;
    //[SerializeField] private ;

    private List<DiaryUiItemData> xItemsList = new();
    private List<DiaryUiItemData> xCharactersList = new();
    private List<DiaryUiItemData> xLocationsList = new();

    private void OnEnable()
    {
        GameManager.Instance.XInteractableEventBus.Register(InteractEventList.ON_DIARY_CHANGE, UpdateDiary);
    }

    private void OnDisable()
    {
        GameManager.Instance.XInteractableEventBus.Unregister(InteractEventList.ON_DIARY_CHANGE, UpdateDiary);
    }

    private void UpdateDiary(params object[] param)
    {
        List<DiaryUiItemData> paramMap = (List<DiaryUiItemData>)param[0];

        foreach (var paramItem in paramMap)
        {
            switch((int)paramItem.xType)
            {
                case 1:
                    InsideGivenListCheck(paramItem, ref xItemsList);
                    break;

                case 2:
                    InsideGivenListCheck(paramItem, ref xCharactersList);
                    break;

                case 3:
                    InsideGivenListCheck(paramItem, ref xLocationsList);
                    break;

                default:
                    Debug.LogError($"the item {paramItem.XModel.sName} does not have a type");
                    break;
            }
        }

        UpdateUI();

        void InsideGivenListCheck(DiaryUiItemData paramItem, ref List<DiaryUiItemData> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (paramItem.XCondition != list[i].XCondition)
                    continue;
                if(paramItem.xType != list[i].xType)
                    continue;

                list[i] = paramItem;
                return;
            }

            list.Add(paramItem);

            return;
        }
    }

    private void UpdateUI()
    {

    }

}


