using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "Custom Assets/ObjectData"),System.Serializable]
    public class ItemDataSO : ScriptableObject
    {
        public OBJECT_TYPE xObjectType;
        public ConditionalItemDataMap xObjectConditonalData;

#if UNITY_EDITOR
        public Conditions xObjectCondition;
        public List<Data> ObjectConditonalData;
#endif // UNITY_EDITOR
    }
}
  



