using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class InkDialoguePlayer : MonoBehaviour
{
    // show in inspector
    public TextMeshProUGUI speakerName, textBox;
    public Button playButton;
    public GameObject dialogBox;
    public Typewriter typewriter;

    [Header("StoryText")]
    [SerializeField] TextAsset storyJSON;
    public Story currentStory;

    [Header("DialogueOptions")]
    [SerializeField] public Button[] choices;
    TextMeshProUGUI[] choicesText;
    [SerializeField] public static List<Tag> tags;

    public UnityEvent tagEvents;

    private void Start()
    {
        EnterStoryFromJSONText(storyJSON.text);
        ContinueStory();

        // get text of each option button and store in array
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void EnterStoryFromJSONText(string json)
    {
        currentStory = new Story(json);
    }

    public void ContinueStory()
    {
        // complete half typed current line
        if (typewriter.isTyping)
        {
            typewriter.StopTyping();
            DisplayLine(currentStory.currentText, false);
        } else
        {
            //move to the next line
            if (currentStory.canContinue)
            {
                string newLine = currentStory.Continue();
                DisplayLine(newLine);

                // check if we have any tags
                if (currentStory.currentTags != null)
                {
                    GetTags(currentStory.currentTags);
                    tagEvents.Invoke();
                }
            } else
            {
                if (currentStory.currentChoices.Count > 0)
                {
                    Debug.LogWarning("Can't Continue Story!");
                } else
                {
                    Debug.Log("reached end of story. closing out dialogue box");
                    dialogBox.SetActive(false);
                }

             }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (!currentStory.canContinue)
        {
            Debug.Log("Making choice at index: " + choiceIndex);
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }

    }

    void TryDisplayChoices()
    {
        HideAllChoices();
        List<Choice> currentChoices = currentStory.currentChoices; // choice is built-in in story class.
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("choices overflow what UI can support, it's " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
    }

    void HideAllChoices()
    {
        foreach(Button choice in choices)
        {
            choice.gameObject.SetActive(false);
        }
    }

    /*
    public void PlayNextLine()
    {
        if (currLine < sentences.Length)
        {
            // parse dialogue string, and then display text
            DisplayLine(sentences[currLine]);

            //prep for next line
            currLine++;
        }
        else
        {
            Debug.Log("Can't play next line because the dialogue has reached the end.");

            // maybe disable the play button?
            playButton.interactable = false;
        }
    }
    */

    private void DisplayLine(string line, bool typewriterIsOn = true)
    {
        // use ':' as a separator in your sentences,
        // marked in SINGLE QUOTES!
        // e.g. "Speaker Name: Your Sentence"
        string[] splitLine = line.Split(':');
        string speaker = splitLine[0];
        string words = splitLine[1];

        //display in UI
        speakerName.text = speaker;
        if (typewriterIsOn)
        {
            typewriter.StartTyping(words);
        } else
        {
            typewriter.DisplayFullText(line);
        }

        TryDisplayChoices();
    }

    void GetTags(List<string> currentTags)
    {
        if (tags != null)
        {
            tags.Clear();
        } else
        {
            tags = new List<Tag>();
        }

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("tag split fault, tag split into " + splitTag.Length + " components.");
            }

            Tag newTag = new Tag();
            newTag.key = splitTag[0].Trim();
            newTag.value = splitTag[1].Trim();

            tags.Add(newTag);
        }
    }
}


[System.Serializable]
public class Tag
{
    public string key;
    public string value;
}
