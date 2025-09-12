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
            //obtain the scriptable object named "ActualDialogueConditions" in Resources folder that contain the player knowing
            ActualDialogueCondition[] actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse");

            bool[] preconditionsCheck = new bool[condition.Count];

            foreach (KeyValuePair<Conditions, int> pair in condition)
            {
                if (!actualConditions[0].MConditions.TryGetValue(pair.Key, out int value))
                    return false;

                if (value != pair.Value)
                    return false;
            }

            return true;
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
                {
                    actualConditions[0].MConditions[pair.Key] = pair.Value;
                }
                else
                {
                    actualConditions[0].MConditions.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}