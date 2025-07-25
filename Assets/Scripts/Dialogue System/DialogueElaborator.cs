using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Misc;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueElaborator : MonoBehaviour
{
    private bool _bIsRunning = false;

    private Dialogue _Dialogue;
    private List<string> _sCurrentText;
    private List<Sprite[]> _xCurrentImage;
    private int _CurrentMonologueIndex;

    // Use this for initialization
    private void Start()
    {
        _sCurrentText = new List<string>();
        _xCurrentImage = new List<Sprite[]>();
        
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.START_DIALOGUE_ELAB, StartDialogue);
    }

    /// <summary>
    /// Method used for starting a specified dialogue, need a list of dialogues with preconditions and a default dialogue
    /// </summary>
    /// <param name="dialogueList"></param>
    /// <param name="defaultDialogue"></param>
    public void StartDialogue(object[] param)
    {
        List<Dialogue> dialogueList = (List<Dialogue>)param[0];
        Dialogue defaultDialogue = (Dialogue)param[1];
        
        if (!_bIsRunning)
        {
            GameManager.Instance.XDialogueEventBus.TriggerEvent("START_DIALOGUE");

            _bIsRunning = true;

            //checking which dialogue from the list is the one who respects the preconditions
            for(int i = 0; i < dialogueList.Count; i++)
            {
                if (CheckPreconditions(dialogueList[i]))
                {
                    _Dialogue = dialogueList[i];
                    break;
                }
            }

            if (_Dialogue == null)
                _Dialogue = defaultDialogue;

            StartMonologue();
        }
    }

    /// <summary>
    /// Start the monologue based on CurrentMonologueIndex and show the first sentence
    /// </summary>
    public void StartMonologue()
    {
        GameManager.Instance.XDialogueEventBus.TriggerEvent("CHANGE_NAME", _Dialogue.DialogueParts[_CurrentMonologueIndex].SName);

        ClearCurrent();

        AddToCurrent(null, null);

        foreach (Sentence sentence in _Dialogue.DialogueParts[_CurrentMonologueIndex].Sentences)
        {
            AddToCurrent(sentence.SSentence, sentence.SImage);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Show the first sentence available in the list of CurrentText
    /// </summary>
    public void DisplayNextSentence()
    {
        //Remove the previous sentence from current
        RemoveFromCurrent();

        if (_sCurrentText.Count == 0)
        {
            //If the list is empty, it means that is needed to go on the next monologue or that the dialogue is ended
            if (_CurrentMonologueIndex < _Dialogue.DialogueParts.Count - 1)
            {
                _CurrentMonologueIndex++;
                StartMonologue();
            }
            else
            {
                if (_Dialogue.HasChoices)
                {
                    System.Collections.Generic.List<DialogueSO[]> listDialogues = new List<DialogueSO[]>();
                    listDialogues.Add(_Dialogue.DialogueChoices);
                    System.Collections.Generic.List<string[]> listLabels = new List<string[]>();
                    listLabels.Add(_Dialogue.LabelsDialogueChoices);
                    EndDialogue();
                    GameManager.Instance.XDialogueEventBus.TriggerEvent("START_CHOICE", listDialogues, listLabels);
                }
                else
                {
                    EndDialogue();
                }
                return;
            }
        }

        TypeSentence();
    }

    /// <summary>
    /// Type letter by letter the entire senteces passed by param in UI
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private void TypeSentence()
    {
        string sentence = "";
        Sprite[] image = null;
        GetFromCurrent(out sentence, out image);
        List<Sprite[]> list = new List<Sprite[]>();
        list.Add(image);

        GameManager.Instance.XDialogueEventBus.TriggerEvent("CHANGE_SENTENCE", "");
        GameManager.Instance.XDialogueEventBus.TriggerEvent("CHANGE_SENTENCE", sentence);
        GameManager.Instance.XDialogueEventBus.TriggerEvent("CHANGE_IMAGE", list);
    }

    /// <summary>
    /// Stop dialogue, reset all to default values and hide text box in UI
    /// </summary>
    private void EndDialogue()
    {
        ApplyPostCondition();

        _CurrentMonologueIndex = 0;
        _Dialogue = null;

        GameManager.Instance.XDialogueEventBus.TriggerEvent("END_DIALOGUE");


        _bIsRunning = false;
    }

    /// <summary>
    /// Method for application of the dialogue's postconditions in the actual player's condition list
    /// </summary>
    private void ApplyPostCondition()
    {
        //obtain the scriptable object named "ActualDialogueConditions" in Resources folder that contain the player knowing
        ActualDialogueCondition[] actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse");

        foreach (KeyValuePair<Conditions, int> pair in _Dialogue.PostConditions)
        {
            if (actualConditions[0].conditions.ContainsKey(pair.Key)) 
                actualConditions[0].conditions[pair.Key] = pair.Value;
            else
                actualConditions[0].conditions.Add(pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// Checker for the preconditions, if is satisfied will be used the actual dialogue, else the default
    /// </summary>
    /// <param name="xDialogue"></param>
    /// <returns></returns>
    private bool CheckPreconditions(Dialogue xDialogue)
    {
        bool check = true;

        bool[] preconditionsCheck = new bool[xDialogue.PreConditions.Count];

        //obtain the scriptable object named "ActualDialogueConditions" in Resources folder that contain the player knowing
        ActualDialogueCondition[] actualConditions = Resources.LoadAll<ActualDialogueCondition>("DialogueSystemInternalUse");

        foreach (KeyValuePair<Conditions, int> pair in xDialogue.PreConditions)
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

    #region current

    private void ClearCurrent()
    {
        _sCurrentText.Clear();
        _xCurrentImage.Clear();
    }

    private void AddToCurrent(string sentence, Sprite[] image)
    {
        _sCurrentText.Add(sentence);
        _xCurrentImage.Add(image);
    }

    private void RemoveFromCurrent()
    {
        _sCurrentText.RemoveAt(0);
        _xCurrentImage.RemoveAt(0);
    }

    private void GetFromCurrent(out string text, out Sprite[] sprite)
    {
        text = _sCurrentText[0];
        sprite = _xCurrentImage[0];
    }

    #endregion
}
