using System.Collections;
using UnityEngine;
using TMPro;

public class MicrogameManager : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;
    [SerializeField] private GameObject slot3;
    [SerializeField] private GameObject slot4;

    [Header("Microgame Prefabs")]
    [SerializeField] private GameObject microgame1Prefab;
    [SerializeField] private GameObject microgame2Prefab;
    [SerializeField] private GameObject microgame3Prefab;
    [SerializeField] private GameObject microgame4Prefab;

    [Header("Timer")]
    [SerializeField] float totalTime = 120f; // 2 minutes default
    [SerializeField] TextMeshProUGUI timerDisplay;

    private float elapsed;
    private bool secondIntervalStarted;

    void Start()
    {
        elapsed = 0f;
        secondIntervalStarted = false;
        LoadFirstInterval();
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
        ActivateMicrogame(slot1, microgame1Prefab);

        if (day >= 2)
            ActivateMicrogame(slot2, microgame2Prefab);

        if (day >= 3)
            ActivateMicrogame(slot3, microgame3Prefab);
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
                ActivateMicrogame(slot2, microgame2Prefab);
                break;
            case 2:
                ActivateMicrogame(slot3, microgame3Prefab);
                break;
            case 3:
                ActivateMicrogame(slot4, microgame4Prefab);
                break;
        }
    }

    void ActivateMicrogame(GameObject slot, GameObject microgamePrefab)
    {
        // Disable the IdleCamera (first child of the slot)
        if (slot.transform.childCount > 0)
        {
            slot.transform.GetChild(0).gameObject.SetActive(false);
        }

        // Instantiate microgame as child of the slot
        Instantiate(microgamePrefab, slot.transform);
    }
}
