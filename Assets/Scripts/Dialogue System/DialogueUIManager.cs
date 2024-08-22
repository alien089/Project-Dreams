using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Sentence;
    [SerializeField] private TextMeshProUGUI _Name;
    [SerializeField] private Image _Image;

    [SerializeField] private EventManager _xEventManager;

    // Start is called before the first frame update
    void Start()
    {
        _xEventManager.Register("CHANGE_SENTENCE", ChangeSentence);
        _xEventManager.Register("CHANGE_NAME", ChangeName);
        _xEventManager.Register("CHANGE_IMAGE", ChangeImage);
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

}
