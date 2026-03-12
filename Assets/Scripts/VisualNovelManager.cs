using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ink.Runtime;
using TMPro;

public class VisualNovelManager : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private TextAsset inkStoryJSON;
    [SerializeField] private string[] knotBeforePerDay;
    [SerializeField] private string[] knotAfterPerDay;

    [Header("Scene Names")]
    [SerializeField] private string microgamesSceneName = "Microgames GEN";
    [SerializeField] private string visualNovelSceneName = "VisualNovelInk";

    [Header("Scene")]
    [SerializeField] private InkDialoguePlayer inkDialoguePlayer;
    [SerializeField] private SpeakerPortraitHandler speakerPortraitHandler;
    [SerializeField] private TextMeshProUGUI dayLabel;

    // Maps GameManager day (1-indexed) to the displayed day number
    private static readonly int[] displayDayMap = { 1, 7, 49 };

    private string pendingTransition;

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

    void UpdateDayLabel(int day)
    {
        if (dayLabel == null) return;
        int index = day - 1;
        int displayDay = (index >= 0 && index < displayDayMap.Length) ? displayDayMap[index] : day;
        dayLabel.text = "Day: " + displayDay;
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

        if (speakerPortraitHandler != null)
        {
            speakerPortraitHandler.UpdateBackground();
        }

        UpdateDayLabel(day);
    }

    public void HandleTags()
    {
        if (InkDialoguePlayer.tags == null) return;

        foreach (Tag tag in InkDialoguePlayer.tags)
        {
            if (tag.key == "transition")
            {
                pendingTransition = tag.value;
            }
        }
    }


    public void OnStoryEnd()
    {
        if (string.IsNullOrEmpty(pendingTransition)) return;

        switch (pendingTransition)
        {
            case "gameplay":
                SceneManager.LoadScene(microgamesSceneName);
                break;
            case "nextday":
                GameManager.Instance.IsAfterGameplay = false;
                GameManager.Instance.AdvanceDay();
                SceneManager.LoadScene(visualNovelSceneName);
                break;
            default:
                Debug.LogError("Unknown transition: " + pendingTransition);
                break;
        }
    }
}
