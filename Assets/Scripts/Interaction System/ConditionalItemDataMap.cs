using UnityEngine;
using UnityEngine.UI;

namespace InteractionSystem
{
    [System.Serializable()]
    public class ConditionalItemDataMap : SerializableDictionaryBase<int, ItemModel> { }


    [System.Serializable]
    public struct ItemModel
    {
        public string sName;
        [TextArea]
        public string sDescription;
        public Sprite xObjectIcon;
        public Sprite xObjectImage;
    }

    public class DiaryUiItemData
    {
        public Conditions XCondition;
        public ItemModel XModel;
        public OBJECT_TYPE xType;

        public DiaryUiItemData(Conditions condition, ItemModel model, OBJECT_TYPE type)
        {
            this.XCondition = condition;
            this.XModel = model;
            this.xType = type;
        }
    }

    [System.Serializable]
    public class Data
    {
        public int index;
        public ItemModel itemModel;
    }
}