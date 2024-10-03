using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public Dialogue Dialogue;

    private void OnValidate()
    {
        for(int i = 0; i < Dialogue.DialogueParts.Length; i++)
        {
            for(int j = 0; j < Dialogue.DialogueParts[i].Sentences.Length; j++)
            {
                if (Dialogue.DialogueParts[i].Sentences[j].SImage.Length != 6)
                {
                    Debug.LogWarning("Don't change the 'ints' field's array size!");
                    Array.Resize(ref Dialogue.DialogueParts[i].Sentences[j]._sImage, 6);
                }
            }
        }
    }
}
