using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueElaborator : MonoBehaviour
{
    private bool _bIsRunning = false;

    [SerializeField] private EventManager _xEventManager;

    private Dialogue _Dialogue;
    private List<string> _sCurrentText;
    private List<Sprite> _xCurrentImage;
    private int m_CurrentMonologueIndex;

    // Use this for initialization
    private void Start()
    {
        _sCurrentText = new List<string>();
        _xCurrentImage = new List<Sprite>();
    }

    /// <summary>
    /// Method used for starting a specified dialogue, need an index based on the dialogue list
    /// </summary>
    /// <param name="actualDialogue"></param>
    public void StartDialogue(Dialogue actualDialogue, Dialogue defaultDialogue)
    {
        if (!_bIsRunning)
        {
            _xEventManager.TriggerEvent("START_DIALOGUE");

            _bIsRunning = true;

            _Dialogue = actualDialogue;

            StartMonologue();
        }
    }

    /// <summary>
    /// Start the monologue based on CurrentMonologueIndex and show the first sentence
    /// </summary>
    public void StartMonologue()
    {
        _xEventManager.TriggerEvent("CHANGE_NAME", _Dialogue.DialogueParts[m_CurrentMonologueIndex].SName);

        ClearCurrent();

        AddToCurrent(null, null);

        foreach (Sentence sentence in _Dialogue.DialogueParts[m_CurrentMonologueIndex].Sentences)
        {
            AddToCurrent(sentence.SSentence, sentence.SImage);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// SHow the first sentence available in the list of CurrentText
    /// </summary>
    public void DisplayNextSentence()
    {
        RemoveFromCurrent();

        if (_sCurrentText.Count == 0)
        {
            if (m_CurrentMonologueIndex < _Dialogue.DialogueParts.Length - 1)
            {
                m_CurrentMonologueIndex++;
                StartMonologue();
            }
            else
            {
                if (_Dialogue.HasChoices)
                {
                    _xEventManager.TriggerEvent("START_CHOICE");

                    m_CurrentMonologueIndex = 0;
                    _bIsRunning = false;
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
        Sprite image = null;
        GetFromCurrent(out sentence, out image);

        _xEventManager.TriggerEvent("CHANGE_SENTENCE", "");
        _xEventManager.TriggerEvent("CHANGE_SENTENCE", sentence);
        _xEventManager.TriggerEvent("CHANGE_IMAGE", image);
    }

    /// <summary>
    /// Stop dialogue, reset all to default values and hide text box in UI
    /// </summary>
    private void EndDialogue()
    {
        m_CurrentMonologueIndex = 0;

        _xEventManager.TriggerEvent("END_DIALOGUE");

        _bIsRunning = false;
    }

    private bool CheckPreconditions()
    {
        bool check = false;



        return check;
    }

    #region current

    private void ClearCurrent()
    {
        _sCurrentText.Clear();
        _xCurrentImage.Clear();
    }

    private void AddToCurrent(string sentence, Sprite image)
    {
        _sCurrentText.Add(sentence);
        _xCurrentImage.Add(image);
    }

    private void RemoveFromCurrent()
    {
        _sCurrentText.RemoveAt(0);
        _xCurrentImage.RemoveAt(0);
    }

    private void GetFromCurrent(out string text, out Sprite sprite)
    {
        text = _sCurrentText[0];
        sprite = _xCurrentImage[0];
    }

    #endregion
}
