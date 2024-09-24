using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueSO> _xDialogues;
    [SerializeField] private DialogueSO _xDefaultDialogue;
    [SerializeField] private DialogueElaborator _xDialogueElaborator;
    // Start is called before the first frame update

    public void StartDialogue()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 0; i < _xDialogues.Count; i++)
        {
            dialogueList.Add(_xDialogues[i].Dialogue);
        }

        _xDialogueElaborator.StartDialogue(dialogueList, _xDefaultDialogue.Dialogue);
    }
}
