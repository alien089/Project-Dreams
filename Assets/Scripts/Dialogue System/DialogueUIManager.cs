using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _MenuBackground;
    [SerializeField] private TextMeshProUGUI _Sentence;
    [SerializeField] private Image _TextBox;
    [SerializeField] private GameObject _NameTextBox;
    [SerializeField] private TextMeshProUGUI _Name;
    [SerializeField] private GameObject _ListOfCharacters;
    private Image[] _Image = new Image[6];
    [SerializeField] private GameObject _ChoiceBox;
    private DialogueTrigger[] _ChoiceTrigger = new DialogueTrigger[3];
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

        for(int i = 0; i < _Image.Length; i++)
        {
            _Image[i] = _ListOfCharacters.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
        for(int i = 0; i < _ChoiceTrigger.Length; i++)
        {
            _ChoiceTrigger[i] = _ChoiceBox.transform.GetChild(i).gameObject.GetComponent<DialogueTrigger>();
        }
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
        List<Sprite[]> list = (List<Sprite[]>)param[0];
        Sprite[] images = list[0];

        for (int i = 0; i < _Image.Length; i++)
        {
            if (images[i] != null)
            {
                _Image[i].gameObject.SetActive(true);
                _Image[i].sprite = images[i];
            }
            else
                _Image[i].gameObject.SetActive(false);
        }
    }

    private void FillChoiceBox(object[] param)
    {
        for(int i = 0; i < _ChoiceTrigger.Length; i++)
        {
            //_ChoiceTrigger[i].XDialogues
        }
    }
    
    private void HideDialogue(object[] param)
    {
        _ContinueBtn.gameObject.SetActive(false);
        _TextBox.gameObject.SetActive(false);
        _MenuBackground.gameObject.SetActive(false);
        _Sentence.text = "";
        _NameTextBox.gameObject.SetActive(false);
        _Name.text = "";
        _ListOfCharacters.gameObject.SetActive(false);
        _ChoicesBox.SetActive(false);
    }    
    
    private void ShowDialogue(object[] param)
    {
        _ContinueBtn.gameObject.SetActive(true);
        _TextBox.gameObject.SetActive(true);
        _MenuBackground.gameObject.SetActive(true);
        _Sentence.text = "";
        _NameTextBox.gameObject.SetActive(true);
        _Name.text = "";
        _ListOfCharacters.gameObject.SetActive(true);
    }

    private void ShowChoices(object[] param)
    {
        _ChoicesBox.SetActive(true);
    }

    private void CreateCharacter(object[] param)
    {
        
    }

    public void HideChoices()
    {
        _ChoicesBox.SetActive(false);
    }
}
