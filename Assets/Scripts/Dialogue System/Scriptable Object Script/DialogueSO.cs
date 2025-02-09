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

    [ConditionalHide("hasChoices")] public string TextDialogueChoiceA;
    [ConditionalHide("hasChoices")] public DialogueSO DialogueChoiceA;
    [ConditionalHide("hasChoices")] public string TextDialogueChoiceB;
    [ConditionalHide("hasChoices")] public DialogueSO DialogueChoiceB;
    [ConditionalHide("hasChoices")] public string TextDialogueChoiceC;
    [ConditionalHide("hasChoices")] public DialogueSO DialogueChoiceC;

    /// <summary>
    /// Ensures that data in the Dialogue object remains consistent and initializes necessary fields.
    /// </summary>
    private void OnValidate()
    {
        // Ensure the Dialogue contains parts; initialize if empty.
        if (Dialogue.DialogueParts.Count == 0) TextElabrotation();

        // Sync choices with the Dialogue object.
        Dialogue.HasChoices = hasChoices;

        Dialogue.LabelsDialogueChoices[0] = TextDialogueChoiceA;
        Dialogue.DialogueChoices[0] = DialogueChoiceA;
        Dialogue.LabelsDialogueChoices[1] = TextDialogueChoiceB;
        Dialogue.DialogueChoices[1] = DialogueChoiceB;
        Dialogue.LabelsDialogueChoices[2] = TextDialogueChoiceC;
        Dialogue.DialogueChoices[2] = DialogueChoiceC;

        // Ensure that each Sentence's `_sImage` array has exactly 6 elements.
        foreach (var part in Dialogue.DialogueParts)
        {
            foreach (var sentence in part.Sentences)
            {
                if (sentence.SImage.Length != 6)
                {
                    Debug.LogWarning("Don't change the 'ints' field's array size!");
                    Array.Resize(ref sentence._sImage, 6);
                }
            }
        }
    }

    /// <summary>
    /// Converts text from a CSV file into Dialogue data.
    /// </summary>
    private void TextElabrotation()
    {
        if (Dialogue.DialogueCSV == null) return;

        TextAsset asset = Dialogue.DialogueCSV;
        string[] lines = asset.text.Split('\n');

        List<Line> ListLines;

        // Parse the CSV file into a list of lines.
        PrepareListLine(out ListLines, lines);

        // Create Dialogue objects from the parsed lines.
        CreateDialogue(ListLines);
    }

    /// <summary>
    /// Parses lines from the CSV file into structured data.
    /// </summary>
    /// <param name="ListLines">Output list of parsed lines.</param>
    /// <param name="lines">Array of CSV file lines.</param>
    private void PrepareListLine(out List<Line> ListLines, string[] lines)
    {
        ListLines = new List<Line>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('_');

            if (parts.Length != 9)
            {
                Debug.LogError("Number of fields incorrect at line: " + i);
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

    /// <summary>
    /// Converts parsed lines into Dialogue objects.
    /// </summary>
    /// <param name="ListLines">List of parsed lines.</param>
    private void CreateDialogue(List<Line> ListLines)
    {
        for (int i = 0; i < ListLines.Count; i++)
        {
            string name = ListLines[i].PGName;
            int j = i + 1;
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

    /// <summary>
    /// Converts sprite references in text form to Sprite objects.
    /// </summary>
    /// <param name="strSprite">Array of sprite paths as strings.</param>
    /// <returns>Array of Sprite objects.</returns>
    private Sprite[] TextToSprite(string[] strSprite)
    {
        Sprite[] xSprite = new Sprite[6];

        for (int i = 0; i < strSprite.Length; i++)
        {
            if (strSprite[i] == " ") continue;

            string strSpritePath = "2D/Character Sprites/";

            string[] strSpriteParts = strSprite[i].Split('-'); // Split sprite string into name and emotion.
            strSpritePath += strSpriteParts[0]; // Get name only.

            strSpritePath += "/S_"; // Prepare file name.
            strSpritePath += strSprite[i];

            // Load sprite from the Resources folder.
            Sprite sprite = Resources.Load<Sprite>(strSpritePath);

            xSprite[i] = sprite;
        }
        return xSprite;
    }

    // Supporting structs and enums

    /// <summary>
    /// Represents a line parsed from the CSV file.
    /// </summary>
    private struct Line
    {
        public string Audio;
        public string[] PG;
        public string PGName;
        public string Sentence;
    }

    /// <summary>
    /// Enum for indexing fields in the CSV file.
    /// </summary>
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
