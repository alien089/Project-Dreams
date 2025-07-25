using System.Collections;
using System.Collections.Generic;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    // Serialized fields for UI components
    [SerializeField] private GameObject _MenuBackground;
    [SerializeField] private TextMeshProUGUI _Sentence;
    [SerializeField] private Image _TextBox;
    [SerializeField] private GameObject _NameTextBox;
    [SerializeField] private TextMeshProUGUI _Name;
    [SerializeField] private GameObject _ListOfCharacters;
    private Image[] _Image = new Image[6];
    [SerializeField] private Button _ContinueBtn; 
    [SerializeField] private GameObject _ChoicesBox;
    [SerializeField] private GameObject _ChoicePrefab;

    // Initialization and event registration
    void Start()
    {
        // Registering functions to EventManager
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.CHANGE_SENTENCE, ChangeSentence);
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.CHANGE_NAME, ChangeName);
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.CHANGE_IMAGE, ChangeImage);
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.END_DIALOGUE, HideDialogue);
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.START_DIALOGUE, ShowDialogue);
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.START_CHOICE, ShowChoices);
        GameManager.Instance.XDialogueEventBus.Register(DialogueEventList.HIDE_CHOICE, HideChoices);

        // Initial UI state: hidden
        HideDialogue(null);

        // Populate _Image array with character portraits
        for (int i = 0; i < _Image.Length; i++)
        {
            _Image[i] = _ListOfCharacters.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }

    /// <summary>
    /// Changes the dialogue sentence displayed in the UI.
    /// </summary>
    /// <param name="param">Array where the first element is the sentence (string).</param>
    public void ChangeSentence(object[] param)
    {
        _Sentence.text = (string)param[0];
    }

    /// <summary>
    /// Updates the character name displayed in the UI.
    /// </summary>
    /// <param name="param">Array where the first element is the name (string).</param>
    public void ChangeName(object[] param)
    {
        _Name.text = (string)param[0];
    }

    /// <summary>
    /// Updates character images shown in the UI based on a list of sprites.
    /// </summary>
    /// <param name="param">Array where the first element is a list of Sprite arrays.</param>
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

    /// <summary>
    /// Fills the choice box with options and their corresponding dialogues.
    /// </summary>
    /// <param name="param">Array containing dialogues and their labels.</param>
    private void FillChoiceBox(object[] param)
    {
        List<DialogueSO[]> listDialogues = (List<DialogueSO[]>)param[0];
        DialogueSO[] dialogues = listDialogues[0];
        List<string[]> listLabels = (List<string[]>)param[1];
        string[] labels = listLabels[0];
        
        for (int i = 0; i < dialogues.Length; i++)
        {
            GameObject tmp = Instantiate(_ChoicePrefab, _ChoicesBox.transform);
            tmp.GetComponent<DialogueTrigger>().XDefaultDialogue = dialogues[i];
            tmp.GetComponentInChildren<TMP_Text>().text = labels[i];
        }
    }
    
    /// <summary>
    /// Hides all dialogue-related UI elements.
    /// </summary>
    /// <param name="param">Optional parameter (not used).</param>
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

    /// <summary>
    /// Displays all dialogue-related UI elements.
    /// </summary>
    /// <param name="param">Optional parameter (not used).</param>
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

    /// <summary>
    /// Displays the choice box and fills it with options.
    /// </summary>
    /// <param name="param">Array containing dialogues and their labels.</param>
    private void ShowChoices(object[] param)
    {
        FillChoiceBox(param);
        _ChoicesBox.SetActive(true);
    }

    /// <summary>
    /// Hides the choice box UI element.
    /// </summary>
    public void HideChoices(object[] param)
    {
        _ChoicesBox.SetActive(false);

        for (int i = 0; i < _ChoicesBox.transform.childCount; i++)
        {
            Destroy(_ChoicesBox.transform.GetChild(i).gameObject);
        }
    }
}
