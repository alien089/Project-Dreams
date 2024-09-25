using UnityEngine;

[System.Serializable()]
public class Condition : SerializableDictionaryBase<Conditions, int> { }


[System.Serializable]
public class Dialogue
{
    //Conditions needed to show this dialogue
    [SerializeField] private Condition _PreConditions;

    //List of monologues
    [SerializeField] private Monologue[] _DialogueParts;

    //Conditions unlocked from this dialogue
    [SerializeField] private Condition _PostConditions;

    //Boolean needed to show multiple choices after the dialogue end
    [SerializeField] private bool _HasChoices;

    public Monologue[] DialogueParts { get => _DialogueParts; } 
    public bool HasChoices { get => _HasChoices; }
    public Condition PreConditions { get => _PreConditions; }
    public Condition PostConditions { get => _PostConditions; }
}

[System.Serializable]
public class Monologue
{
    [SerializeField] private string _sName;
    [SerializeField] private Sentence[] _Sentences;

    public string SName { get => _sName; }
    public Sentence[] Sentences { get => _Sentences; }
}

[System.Serializable]
public class Sentence
{
    [SerializeField] private string _sSentence;
    [SerializeField] private AudioClip _sAudio;
    [SerializeField] private Sprite _sImage;

    public string SSentence { get => _sSentence;}
    public AudioClip SAudio { get => _sAudio;}
    public Sprite SImage { get => _sImage;}

    public Sentence(string sSentence, Sprite sImage)
    {
        _sSentence = sSentence;
        _sImage = sImage;
    }
}