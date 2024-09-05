using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/DialogueConditions")]
public class ActualDialogueCondition : ScriptableObject
{
    public List<Conditions> conditions;
}

public enum Conditions
{
    None
}