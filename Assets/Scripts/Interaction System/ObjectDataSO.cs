using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "Custom Assets/ObjectData")]
    public class ObjectDataSO : ScriptableObject
    {
        public Conditions xObjectCondition;
        public OBJECT_TYPE xObjectType;
        public ConditionalObjectDataMap xObjectConditonalData;
    }
}
