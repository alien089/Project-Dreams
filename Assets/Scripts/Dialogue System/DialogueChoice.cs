using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue_System
{
    public class DialogueChoice : MonoBehaviour
    {
        private Button _xChoiceBtn;
        private DialogueTrigger _xDialogueTrigger;

        private void Start()
        {
            _xChoiceBtn = gameObject.GetComponent<Button>();
            _xDialogueTrigger = gameObject.GetComponent<DialogueTrigger>();
            
            _xChoiceBtn.onClick.AddListener(HideChoice);
            _xChoiceBtn.onClick.AddListener(_xDialogueTrigger.StartDialogue);
        }

        private void HideChoice()
        {
            GameManager.Instance.XDialogueEventBus.TriggerEvent(DialogueEventList.HIDE_CHOICE);
        }
    }
}