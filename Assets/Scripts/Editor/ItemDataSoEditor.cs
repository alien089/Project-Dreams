using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InteractionSystem
{
    [CustomEditor(typeof(ItemDataSO)), CanEditMultipleObjects]
    public class ItemDataSoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ItemDataSO itemData = (ItemDataSO)target;
            
            serializedObject.Update();
           
            EditorGUILayout.PropertyField(serializedObject.FindProperty("xObjectCondition"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("xObjectType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectConditonalData"), true);
            serializedObject.ApplyModifiedProperties();

            itemData.xObjectConditonalData = ToDictionary();
        }
        public ConditionalItemDataMap ToDictionary()
        {
            ItemDataSO itemData = (ItemDataSO)target;
            itemData.xObjectConditonalData = new();
            ConditionalItemDataMap map = new();
            for (int i = 0; i < itemData.ObjectConditonalData.Count; i++)
            {
                Data data = itemData.ObjectConditonalData[i];
                if (!map.TryAdd(data.index, data.itemModel))
                    Debug.LogWarning($"the index value of element {i-1} in the list is the same as another change it or this will be excluded from the map");
            }

            return map;
        }


    }

}
