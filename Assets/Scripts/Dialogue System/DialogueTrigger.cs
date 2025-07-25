using System.Collections;
using System.Collections.Generic;
using Misc;
using UnityEngine;

/// <summary>
/// Handles triggering dialogues by using predefined dialogue assets and a dialogue elaborator.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    // Serialized fields for Unity Inspector
    [SerializeField] private List<DialogueSO> _xDialogues; // List of dialogue scriptable objects
    [SerializeField] private DialogueSO _xDefaultDialogue; // Default dialogue if no specific dialogues are available

    // Properties for external access
    public List<DialogueSO> XDialogues { get => _xDialogues; set => _xDialogues = value; }
    public DialogueSO XDefaultDialogue { get => _xDefaultDialogue; set => _xDefaultDialogue = value; }

    // ------------------------ MAIN FUNCTIONALITY ------------------------

    /// <summary>
    /// Initiates the dialogue sequence by collecting all dialogues from the list and passing them 
    /// to the DialogueElaborator along with the default dialogue.
    /// </summary>
    public void StartDialogue()
    {
        // Create a list to store dialogues to be processed
        List<Dialogue> dialogueList = new List<Dialogue>();

        // Add dialogues from the serialized list to the local list
        for (int i = 0; i < _xDialogues.Count; i++)
        {
            dialogueList.Add(_xDialogues[i].Dialogue);
        }

        if (dialogueList.Count == 0)
        {
            dialogueList.Add(_xDefaultDialogue.Dialogue);
        }

        // Pass the dialogues to the DialogueElaborator for processing
        GameManager.Instance.XDialogueEventBus.TriggerEvent(DialogueEventList.START_DIALOGUE_ELAB, dialogueList, _xDefaultDialogue.Dialogue);
    }
}