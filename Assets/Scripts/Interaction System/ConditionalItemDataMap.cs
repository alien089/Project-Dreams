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

    [System.Serializable]
    public class Data
    {
        public int index;
        public ItemModel itemModel;
    }
}