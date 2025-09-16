using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

namespace InteractionSystem
{
    [CustomEditor(typeof(DiaryManager))]
    public class DiaryManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DiaryManager manager = (DiaryManager)target;

            if (GUILayout.Button("get all scriptable objects"))
            {
                ItemDataSO[] resources = Resources.LoadAll<ItemDataSO>("IteractionSystem/TestScriptableObjects");

                foreach (ItemDataSO obj in resources)
                {
                    if (manager.map.ContainsKey(obj.xObjectCondition))
                        continue;

                    manager.map.Add(obj.xObjectCondition, obj);
                }
            }
        }


    }
}
