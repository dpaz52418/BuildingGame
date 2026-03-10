using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int totalDays = 3;

    private int currentDay = 1;
    public int CurrentDay => currentDay;
    public int TotalDays => totalDays;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AdvanceDay()
    {
        if (currentDay < totalDays)
        {
            currentDay++;
        }
        else
        {
            Debug.Log("Final day reached.");
        }
    }

    public void ResetDays()
    {
        currentDay = 1;
    }
}
