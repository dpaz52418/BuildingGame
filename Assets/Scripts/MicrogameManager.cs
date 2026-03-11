using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MicrogameManager : MonoBehaviour
{
    public static event Action<GameObject> OnMinigameLost;

    /// <summary>
    /// Call from any script inside a minigame hierarchy to report a loss.
    /// Pass any GameObject that lives under the minigame's slot.
    /// </summary>
    public static void NotifyLoss(GameObject losingObject)
    {
        OnMinigameLost?.Invoke(losingObject);
    }

    [Header("Slots")]
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;
    [SerializeField] private GameObject slot3;
    [SerializeField] private GameObject slot4;

    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject display1;
    [SerializeField] private GameObject display2;
    [SerializeField] private GameObject display3;
    [SerializeField] private GameObject display4;

    [Header("Microgame Prefabs")]
    [SerializeField] private GameObject microgame1Prefab;
    [SerializeField] private GameObject microgame2Prefab;
    [SerializeField] private GameObject microgame3Prefab;
    [SerializeField] private GameObject microgame4Prefab;

    [Header("Timer")]
    [SerializeField] float totalTime = 120f; // 2 minutes default
    [SerializeField] TextMeshProUGUI timerDisplay;

    [Header("Lives")]
    [SerializeField] private int maxLives = 4;
    [SerializeField] private Image[] heartImages; // assign 4 heart Images in Inspector

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel; // a UI panel with a retry button

    private int currentLives;
    private float elapsed;
    private bool secondIntervalStarted;
    private bool isGameOver;

    // maps each slot to its prefab and display so we can reinstantiate
    private Dictionary<GameObject, SlotInfo> slotMap = new Dictionary<GameObject, SlotInfo>();

    private struct SlotInfo
    {
        public GameObject prefab;
        public GameObject display;
    }

    void OnEnable()
    {
        OnMinigameLost += HandleMinigameLost;
    }

    void OnDisable()
    {
        OnMinigameLost -= HandleMinigameLost;
    }

    void Start()
    {
        currentLives = maxLives;
        elapsed = 0f;
        secondIntervalStarted = false;
        isGameOver = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateHeartsUI();
        LoadFirstInterval();
    }

    void HandleMinigameLost(GameObject losingObject)
    {
        if (isGameOver) return;

        // walk up the hierarchy to find which slot this object belongs to
        GameObject slot = FindSlotForObject(losingObject);

        //currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            isGameOver = true;
            ShowGameOver();
            return;
        }

        // reinstantiate the lost minigame
        if (slot != null && slotMap.ContainsKey(slot))
        {
            ReinstantiateMicrogame(slot);
        }
    }

    GameObject FindSlotForObject(GameObject obj)
    {
        if (obj == null) return null;

        Transform current = obj.transform;
        while (current != null)
        {
            if (current.parent != null && slotMap.ContainsKey(current.parent.gameObject))
            {
                return current.parent.gameObject;
            }
            current = current.parent;
        }
        return null;
    }

    void ReinstantiateMicrogame(GameObject slot)
    {
        SlotInfo info = slotMap[slot];

        // destroy all current children of the slot
        for (int i = slot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(slot.transform.GetChild(i).gameObject);
        }

        // spawn fresh
        Instantiate(info.prefab, slot.transform);
    }

    void ShowGameOver()
    {
        Debug.Log("Game Over! All lives lost.");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    /// <summary>
    /// Hook this up to the Retry button's OnClick event.
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateHeartsUI()
    {
        if (heartImages == null) return;
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] != null)
                heartImages[i].enabled = i < currentLives;
        }
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        float intervalDuration = totalTime / 2f;

        if (!secondIntervalStarted && elapsed >= intervalDuration)
        {
            secondIntervalStarted = true;
            LoadSecondInterval();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (timerDisplay == null) return;

        float remaining = Mathf.Max(0f, totalTime - elapsed);
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);
        timerDisplay.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    void LoadFirstInterval()
    {
        int day = GameManager.Instance.CurrentDay;

        // Day 1: Slot1
        // Day 2: Slot1, Slot2
        // Day 3: Slot1, Slot2, Slot3
        ActivateMicrogame(slot1, microgame1Prefab, display1);

        if (day >= 2)
            ActivateMicrogame(slot2, microgame2Prefab, display2);

        if (day >= 3)
            ActivateMicrogame(slot3, microgame3Prefab, display3);
    }

    void LoadSecondInterval()
    {
        int day = GameManager.Instance.CurrentDay;

        // Day 1: Slot2
        // Day 2: Slot3
        // Day 3: Slot4
        switch (day)
        {
            case 1:
                ActivateMicrogame(slot2, microgame2Prefab, display2);
                break;
            case 2:
                ActivateMicrogame(slot3, microgame3Prefab, display3);
                break;
            case 3:
                ActivateMicrogame(slot4, microgame4Prefab, display4);
                break;
        }
    }

    void ActivateMicrogame(GameObject slot, GameObject microgamePrefab, GameObject display)
    {
        // register the slot mapping so we can reinstantiate later
        slotMap[slot] = new SlotInfo { prefab = microgamePrefab, display = display };

        // Disable the first child of the corresponding display
        if (display != null && display.transform.childCount > 0)
        {
            display.transform.GetChild(0).gameObject.SetActive(false);
        }

        // Instantiate microgame as child of the slot
        Instantiate(microgamePrefab, slot.transform);
    }
}
