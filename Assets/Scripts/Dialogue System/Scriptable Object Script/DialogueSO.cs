using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public Dialogue Dialogue;
    public bool hasChoices = false;

    private void OnValidate()
    {
        if (Dialogue.DialogueParts.Count == 0) TextElabrotation();
        
        Dialogue.HasChoices = hasChoices;
        
        for (int i = 0; i < Dialogue.DialogueParts.Count; i++)
        {
            for(int j = 0; j < Dialogue.DialogueParts[i].Sentences.Count; j++)
            {
                if (Dialogue.DialogueParts[i].Sentences[j].SImage.Length != 6)
                {
                    Debug.LogWarning("Don't change the 'ints' field's array size!");
                    Array.Resize(ref Dialogue.DialogueParts[i].Sentences[j]._sImage, 6);
                }
            }
        }
    }

    private void TextElabrotation()
    {
        if (Dialogue.DialogueCSV == null) return;

        TextAsset asset = Dialogue.DialogueCSV;
        string[] lines = asset.text.Split('\n');

        List<Line> ListLines;

        PrepareListLine(out ListLines, lines);

        CreateDialogue(ListLines);
    }

    private void PrepareListLine(out List<Line> ListLines, string[] lines)
    {
        ListLines = new List<Line>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('_');

            if (parts.Length != 9)
            {
                Debug.LogError("Numer of fields incorrect at line: " + i);
                return;
            }

            Line line = new Line();

            line.PG = new string[6];

            line.Audio = parts[(int)Field.Audio];

            for (int x = 0; x < 6; x++)
            {
                line.PG[x] = parts[1 + x];
            }

            line.PGName = parts[(int)Field.PGName];
            line.Sentence = parts[(int)Field.Sentence];
            ListLines.Add(line);
        }
    }

    private void CreateDialogue(List<Line> ListLines)
    {
        for (int i = 0; i < ListLines.Count; i++)
        {
            string name = ListLines[i].PGName;
            int j = i;
            j++;
            List<Sentence> strSentences = new List<Sentence>();

            Sentence strSentence = new Sentence(ListLines[i].Sentence, TextToSprite(ListLines[i].PG));
            strSentences.Add(strSentence);

            while (j < ListLines.Count)
            {
                if (ListLines[j].PGName != " ") break;
                strSentence = new Sentence(ListLines[j].Sentence, TextToSprite(ListLines[j].PG));
                strSentences.Add(strSentence);
                j++;
            }

            if (j - 1 != i) i = --j;

            Monologue monologue = new Monologue(name, strSentences);
            Dialogue.DialogueParts.Add(monologue);
        }
    }

    private Sprite[] TextToSprite(string[] strSprite)
    {
        Sprite[] xSprite = new Sprite[6];

        for (int i = 0; i < strSprite.Length; i++)
        {
            if (strSprite[i] == " ") continue;

            string strSpritePath = "2D/Character Sprites/";

            string[] strSpriteParts = strSprite[i].Split('-'); //separa stringa sprite in nome + emozione
            strSpritePath += strSpriteParts[0]; //prendo solo nome

            strSpritePath += "/S_"; //prepara nome file
            strSpritePath += strSprite[i];


            Sprite sprite = Resources.Load<Sprite>(strSpritePath);  //load chiede /.../nome file

            xSprite[i] = sprite;
        }
        return xSprite;
    }

    private struct Line
    {
        public string Audio;
        public string[] PG;
        public string PGName;
        public string Sentence;
    }

    private enum Field
    {
        Audio,
        PG1,
        PG2,
        PG3,
        PG4,
        PG5,
        PG6,
        PGName,
        Sentence
    }
}
