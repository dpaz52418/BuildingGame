using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class VisualNovelManager : MonoBehaviour
{
    [Header("Day-Indexed Assets (index 0 = Day 1, etc.)")]
    [SerializeField] private TextAsset[] inkStoryPerDay;       // Ink JSON per day
    [SerializeField] private Sprite[] backgroundPerDay;        // background image per day
    [SerializeField] private Sprite[] characterPortraitPerDay; // default portrait per day

    [Header("Scene References")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image portraitImage;
    [SerializeField] private InkDialoguePlayer inkDialoguePlayer;

    void Start()
    {
        int day = 1;
        if (GameManager.Instance != null)
        {
            day = GameManager.Instance.CurrentDay;
        }
        else
        {
            Debug.LogWarning("GameManager not found, defaulting to Day 1.");
        }

        LoadDay(day);
    }

    public void LoadDay(int day)
    {
        int index = day - 1; // arrays are 0-based, days are 1-based

        // load Ink story
        if (index < inkStoryPerDay.Length && inkStoryPerDay[index] != null)
        {
            inkDialoguePlayer.EnterStoryFromJSONText(inkStoryPerDay[index].text);
            inkDialoguePlayer.ContinueStory();
        }
        else
        {
            Debug.LogError("No Ink story assigned for Day " + day);
        }

        // load background
        if (backgroundImage != null && index < backgroundPerDay.Length && backgroundPerDay[index] != null)
        {
            backgroundImage.sprite = backgroundPerDay[index];
        }

        // load character portrait
        if (portraitImage != null && index < characterPortraitPerDay.Length && characterPortraitPerDay[index] != null)
        {
            portraitImage.sprite = characterPortraitPerDay[index];
        }
    }
}
