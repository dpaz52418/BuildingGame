using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class VisualNovelManager : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private TextAsset inkStoryJSON;           // single Ink JSON for all days
    [SerializeField] private string[] knotBeforePerDay;        // "before gameplay" knot per day (index 0 = Day 1)
    [SerializeField] private string[] knotAfterPerDay;         // "after gameplay" knot per day (index 0 = Day 1)
    [SerializeField] private Sprite[] backgroundPerDay;        // background image per day
   // [SerializeField] private Sprite[] characterPortraitPerDay; // default portrait per day

    [Header("Scene")]
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
        int index = day - 1;
        bool after = GameManager.Instance != null && GameManager.Instance.IsAfterGameplay;

        string[] knots = after ? knotAfterPerDay : knotBeforePerDay;

        // load Ink story and jump to the correct knot
        if (inkStoryJSON != null && index < knots.Length && !string.IsNullOrEmpty(knots[index]))
        {
            inkDialoguePlayer.EnterStoryFromJSONText(inkStoryJSON.text);
            inkDialoguePlayer.currentStory.ChoosePathString(knots[index]);
            inkDialoguePlayer.ContinueStory();
        }
        else
        {
            Debug.LogError("no knot assigned for Day " + day + (after ? " (after)" : " (before)"));
        }

        if (backgroundImage != null && index < backgroundPerDay.Length && backgroundPerDay[index] != null)
        {
            backgroundImage.sprite = backgroundPerDay[index];
        }
        /*
        if (portraitImage != null && index < characterPortraitPerDay.Length && characterPortraitPerDay[index] != null)
        {
            portraitImage.sprite = characterPortraitPerDay[index];
        }
        */
    }
}
