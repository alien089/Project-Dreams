using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class Condition : SerializableDictionaryBase<Conditions, int> { }


[System.Serializable]
public class Dialogue
{
    //Conditions needed to show this dialogue
    [SerializeField] private Condition _PreConditions;

    [SerializeField] private TextAsset _DialogueCSV;
    //List of monologues
    [SerializeField] private List<Monologue> _DialogueParts;

    //Conditions unlocked from this dialogue
    [SerializeField] private Condition _PostConditions;

    //Boolean needed to show multiple choices after the dialogue end
    private bool _HasChoices;
    private DialogueSO[] _DialogueChoices = new DialogueSO[3];


    public List<Monologue> DialogueParts { get => _DialogueParts; } 
    public bool HasChoices { get => _HasChoices;  set => _HasChoices = value; }
    public DialogueSO[] DialogueChoices { get => _DialogueChoices;  set => _DialogueChoices = value; }
    public Condition PreConditions { get => _PreConditions; }
    public Condition PostConditions { get => _PostConditions; }
    public TextAsset DialogueCSV { get => _DialogueCSV; set => _DialogueCSV = value; }
}

[System.Serializable]
public class Monologue
{
    [SerializeField] private string _sName;
    [SerializeField] private List<Sentence> _Sentences;

    public string SName { get => _sName; }
    public List<Sentence> Sentences { get => _Sentences; }

    public Monologue(string sName, List<Sentence> sentences)
    {
        _sName = sName;
        _Sentences = sentences;
    }
}


[System.Serializable]
public class Sentence
{
    [SerializeField] private string _sSentence;
    [SerializeField] private AudioClip _sAudio;
    [SerializeField] public Sprite[] _sImage = new Sprite[6];

    public string SSentence { get => _sSentence;}
    public AudioClip SAudio { get => _sAudio;}
    public Sprite[] SImage { get => _sImage;}

    public Sentence(string sSentence, Sprite[] sImage)
    {
        _sSentence = sSentence;
        _sImage = sImage;
    }
}