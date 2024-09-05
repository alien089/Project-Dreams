using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSO _xDialogue;
    [SerializeField] private DialogueSO _xDefaultDialogue;
    [SerializeField] private DialogueElaborator _xDialogueElaborator;
    // Start is called before the first frame update

    public void StartDialogue()
    {
        _xDialogueElaborator.StartDialogue(_xDialogue.Dialogue, _xDefaultDialogue.Dialogue);
    }
}
