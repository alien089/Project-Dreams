using UnityEngine.UI;

namespace InteractionSystem
{
    public class ConditionalItemDataMap : SerializableDictionaryBase<int, ObjectModel> { }

    [System.Serializable]
    public struct ObjectModel
    {
        public string sName;
        public string sDescription;
        public Image xObjectIcon;
        public Image xObjectImage;
    }
}