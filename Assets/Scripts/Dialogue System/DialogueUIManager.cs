using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Sentence;
    [SerializeField] private Image _TextBox;
    [SerializeField] private TextMeshProUGUI _Name;
    [SerializeField] private Image _Image;
    [SerializeField] private Button _ContinueBtn; 
    [SerializeField] private GameObject _ChoicesBox;

    [SerializeField] private EventManager _xEventManager;

    // Start is called before the first frame update
    void Start()
    {
        _xEventManager.Register("CHANGE_SENTENCE", ChangeSentence);
        _xEventManager.Register("CHANGE_NAME", ChangeName);
        _xEventManager.Register("CHANGE_IMAGE", ChangeImage);
        _xEventManager.Register("END_DIALOGUE", HideDialogue);
        _xEventManager.Register("START_DIALOGUE", ShowDialogue);
        _xEventManager.Register("START_CHOICE", ShowChoices);

        HideDialogue(null);
    }

    public void ChangeSentence(object[] param)
    {
        _Sentence.text = (string)param[0];
    }
    public void ChangeName(object[] param)
    {
        _Name.text = (string)param[0];
    }
    
    public void ChangeImage(object[] param)
    {
        _Image.sprite = (Sprite)param[0];
    }
    
    private void HideDialogue(object[] param)
    {
        _ContinueBtn.gameObject.SetActive(false);
        _TextBox.gameObject.SetActive(false);
        _Sentence.text = "";
        _Name.text = "";
        _Image.gameObject.SetActive(false);
        _ChoicesBox.SetActive(false);
    }    
    
    private void ShowDialogue(object[] param)
    {
        _ContinueBtn.gameObject.SetActive(true);
        _TextBox.gameObject.SetActive(true);
        _Sentence.text = "";
        _Name.text = "";
        _Image.gameObject.SetActive(true);
    }

    private void ShowChoices(object[] param)
    {
        _ChoicesBox.SetActive(true);
    }

    public void HideChoices()
    {
        _ChoicesBox.SetActive(false);
    }
}
