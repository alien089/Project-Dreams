using System.Collections.Generic;
using UnityEngine;

namespace Progress_System
{
    [System.Serializable()]
    public class Condition : SerializableDictionaryBase<Conditions, int> { }
    
    public static class ConditionsUtils
    {
        /// <summary>
        /// Checker for the preconditions, if is satisfied will be used the actual dialogue, else the default
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
                if (actualConditions[0].conditions.TryGetValue(pair.Key, out int value))
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
        /// Method for application of the dialogue's postconditions in the actual player's condition list
        /// </summary>
        public static void ApplyCondition(Condition condition)
        {
            //obtain the scriptable object named "ActualDialogueConditions" in Resources folder that contain the player knowing
            ActualDialogueCondition[] actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse");

            foreach (KeyValuePair<Conditions, int> pair in condition)
            {
                if (actualConditions[0].conditions.ContainsKey(pair.Key)) 
                    actualConditions[0].conditions[pair.Key] = pair.Value;
                else
                    actualConditions[0].conditions.Add(pair.Key, pair.Value);
            }
        }
    }
}