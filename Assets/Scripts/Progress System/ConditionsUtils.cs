using System.Collections.Generic;
using UnityEngine;

namespace Progress_System
{
    [System.Serializable()]
    public class Condition : SerializableDictionaryBase<Conditions, int> { }
    
    public static class ConditionsUtils
    {
        /// <summary>
        /// Checker for the given conditions
        /// </summary>
        /// <param name="xDialogue"></param>
        /// <returns></returns>
        public static bool CheckConditions(Condition condition)
        {
            bool check = true;

            bool[] preconditionsCheck = new bool[condition.Count];

            //obtain the scriptable object named "ActualDialogueConditions" in Resources folder that contain the player knowing
            ActualDialogueCondition[] actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse");

            foreach (KeyValuePair<Conditions, int> pair in condition)
            {
                if (actualConditions[0].MConditions.TryGetValue(pair.Key, out int value))
                {
                    if (value == pair.Value)
                    {
                        for (int i = 0; i < preconditionsCheck.Length; i++)
                        {
                            if (preconditionsCheck[i] == false)
                            {
                                preconditionsCheck[i] = true;
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < preconditionsCheck.Length; i++)
            {
                if (preconditionsCheck[i] == false)
                {
                    check = false;
                    break;
                }
            }

            return check;
        }
        
        /// <summary>
        /// Method for application of the given conditions in the actual player's condition list
        /// </summary>
        public static void ApplyCondition(Condition condition)
        {
            //obtain the scriptable object named "ActualDialogueConditions" in Resources folder that contain the player knowing
            ActualDialogueCondition[] actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse");

            foreach (KeyValuePair<Conditions, int> pair in condition)
            {
                if (actualConditions[0].MConditions.ContainsKey(pair.Key))
                    actualConditions[0].MConditions[pair.Key] = pair.Value;
                else
                    actualConditions[0].MConditions.Add(pair.Key, pair.Value);
            }
        }
    }
}