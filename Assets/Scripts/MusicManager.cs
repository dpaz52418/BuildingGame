using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Visual Novel Music")]
    [SerializeField] private AudioClip visualNovelClip;
    [SerializeField] private AudioClip day3Clip; // plays during day_3 and day_3_after

    [Header("Gameplay Music")]
    [SerializeField] private AudioClip gameplayClip;

    [Header("Settings")]
    [SerializeField] private float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = volume;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clip = PickClip(scene.name);

        if (clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    AudioClip PickClip(string sceneName)
    {
        // Gameplay scene
        if (sceneName.Contains("Microgames"))
        {
            return gameplayClip;
        }

        // Visual novel scene
        if (sceneName.Contains("VisualNovel"))
        {
            if (GameManager.Instance != null && GameManager.Instance.CurrentDay == 3)
            {
                return day3Clip;
            }
            return visualNovelClip;
        }

        // Title screen or unknown — no music change
        return null;
    }
}
