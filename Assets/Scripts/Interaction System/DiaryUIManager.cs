using InteractionSystem;
using Misc;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUIManager : MonoBehaviour
{
    private List<ItemModel> items = new();

    private void OnEnable()
    {
        GameManager.Instance.XInteractableEventBus.Register(InteractEventList.ON_DIARY_CHANGE, UpdateDiary);
    }

    private void UpdateDiary(params object[] param)
    {
        foreach (object x in param)
        {
            ItemModel paramItem = (ItemModel)x;

            foreach(ItemModel listItem in items)
            {
                // find if you already have it stored
            }

        }
    }
}
