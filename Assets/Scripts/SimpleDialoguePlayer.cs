using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleDialoguePlayer: MonoBehaviour
{
    // show in inspector
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI textBox;
    [SerializeField] string[] sentences;
    public Button playButton;
    public Typewriter typewriter;

    // private variables
    int currLine = 0;

    private void Start()
    {
        PlayNextLine();
    }

    public void PlayNextLine()
    {
        if (currLine < sentences.Length)
        {
            // parse dialogue string, and then display text
            DisplayLine(sentences[currLine]);

            //prep for next line
            currLine++;
        } else
        {
            Debug.Log("Can't play next line because the dialogue has reached the end.");

            // maybe disable the play button?
            playButton.interactable = false;
        }
    }

    private void DisplayLine(string line)
    {
        // use ':' as a separator in your sentences,
        // marked in SINGLE QUOTES!
        // e.g. "Speaker Name: Your Sentence"
        string[] splitLine = line.Split(':');
        string speaker = splitLine[0];
        string words = splitLine[1];

        //display in UI
        speakerName.text = speaker;
        //textBox.text = words;
        typewriter.StartTyping(words);
    }

}
