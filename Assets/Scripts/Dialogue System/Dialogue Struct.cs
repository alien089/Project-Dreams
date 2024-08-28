using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private PreConditions[] _PreConditions;

    [SerializeField] private Monologue[] _DialogueParts;

    [SerializeField] private PostConditions[] PostConditions;

    [SerializeField] private bool _bHasChoices;

    public Monologue[] DialogueParts { get => _DialogueParts; }
    public bool BHasChoices { get => _bHasChoices; }
    public PreConditions[] PreConditions { get => _PreConditions; }
    public PostConditions[] PostConditions1 { get => PostConditions; }
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