using Misc;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace InteractionSystem
{
    public class DiaryUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject DiaryUiPannel;

        [SerializeField] private Image xItemPortrait;
        [SerializeField] private TextMeshProUGUI xDescription;
        [SerializeField] private Sprite xItemSlotDefaultSprite;

        [SerializeField] private ItemSlot[] xItemSlots = new ItemSlot[16];

        private List<DiaryUiItemData> _xItemsList = new();
        private List<DiaryUiItemData> _xCharactersList = new();
        private List<DiaryUiItemData> _xLocationsList = new();

        private int _iPageIndex = 0;

        private bool _bHasLoaded;

        private OBJECT_TYPE _xCurrentUIType = OBJECT_TYPE.Item;


        private void OnEnable()
        {
            GameManager.Instance.XInteractableEventBus.Register(InteractEventList.ON_DIARY_CHANGE, UpdateDiary);
            GameManager.Instance.XInteractableEventBus.Register(InteractEventList.ON_ITEMSLOT_PRESSED, OnItemSlotPressed);
        }


        private void OnDisable()
        {
            GameManager.Instance.XInteractableEventBus.Unregister(InteractEventList.ON_DIARY_CHANGE, UpdateDiary);
            GameManager.Instance.XInteractableEventBus.Unregister(InteractEventList.ON_ITEMSLOT_PRESSED, OnItemSlotPressed);
        }

        private void UpdateDiary(params object[] param)
        {
            List<DiaryUiItemData> paramMap = (List<DiaryUiItemData>)param[0];

            foreach (var paramItem in paramMap)
            {
                switch ((int)paramItem.xType)
                {
                    case 1:
                        InsideGivenListCheck(paramItem, ref _xItemsList);
                        LoadUI(ref _xItemsList);
                        break;

                    case 2:
                        InsideGivenListCheck(paramItem, ref _xCharactersList);
                        LoadUI(ref _xCharactersList);
                        break;

                    case 3:
                        InsideGivenListCheck(paramItem, ref _xLocationsList);
                        LoadUI(ref _xLocationsList);
                        break;

                    default:
                        Debug.LogError($"the item {paramItem.xModel.sName} does not have a type");
                        break;
                }
            }

            void InsideGivenListCheck(DiaryUiItemData paramItem, ref List<DiaryUiItemData> list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (paramItem.xCondition != list[i].xCondition)
                        continue;
                    if (paramItem.xType != list[i].xType)
                        continue;

                    list[i] = paramItem;
                    return;
                }
                list.Add(paramItem);
                return;
            }
        }

        private void LoadUI(ref List<DiaryUiItemData> list)
        {
            var currentPage = (xItemSlots.Length - 1) * _iPageIndex;

            #region |loop the pages|
            if (currentPage >= list.Count)
            {
                _iPageIndex = 0;
                currentPage = (xItemSlots.Length - 1) * _iPageIndex;
            }

            else if (currentPage < 0)
            {
                _iPageIndex = list.Count / xItemSlots.Length;
                currentPage = (xItemSlots.Length - 1) * _iPageIndex;
            }
            #endregion |loop the pages|

            int itemSlotIndex = 0;

            for (int i = currentPage; i < list.Count; i++)
            {
                if (i >= list.Count)
                    break;

                ItemModel listItem = list[i].xModel;

                // change the icons of the items
                xItemSlots[itemSlotIndex].xIcon.sprite = listItem.xObjectIcon;
                xItemSlots[itemSlotIndex].xName.text = listItem.sName;
                xItemSlots[itemSlotIndex].xItemImage = listItem.xObjectImage;
                xItemSlots[i].sDescription = listItem.sDescription;

                itemSlotIndex++;
            }

            _bHasLoaded = true;

            if (itemSlotIndex >= xItemSlots.Length)
                return;

            for (int i = itemSlotIndex; i < xItemSlots.Length; i++)
            {
                xItemSlots[i].xIcon.sprite = xItemSlotDefaultSprite;
                xItemSlots[i].xName.text = "";
            }
        }

        public void OpenUI()
        {
            if (_bHasLoaded)
            {
                DiaryUiPannel.SetActive(true);
                return;
            }

            switch ((int)_xCurrentUIType)
            {
                case 1:
                    LoadUI(ref _xItemsList);
                    break;

                case 2:
                    LoadUI(ref _xCharactersList);
                    break;

                case 3:
                    LoadUI(ref _xLocationsList);
                    break;

                default:
                    Debug.LogError($"how did this even happen? error in openUI no OBJECT_TYPE");
                    break;
            }

            DiaryUiPannel.SetActive(true);
        }

        public void CloseUI()
        {
            DiaryUiPannel.SetActive(false);
        }

        /// <summary>
        /// changes the item loaded
        /// </summary>
        /// <param name="pageTurnDirection">-1 = previous page | 1 = next page </param>
        public void ChangePage(int pageTurnDirection)
        {
            _iPageIndex += pageTurnDirection;
            LoadUI(ref _xItemsList);
        }

        public void ChangeType(OBJECT_TYPE type)
        {
            _xCurrentUIType = type;
            _iPageIndex = 0;
            LoadUI(ref _xItemsList);
        }

        private void OnItemSlotPressed(object[] obj)
        {
            ItemSlot itemSlot = (ItemSlot)obj[0];

            xItemPortrait.sprite = itemSlot.xItemImage;
            xDescription.text = itemSlot.sDescription;
        }


    }
}