using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenTransitionManager : MonoBehaviour
{
    // Reference to the Canvas Group component
    public CanvasGroup canvasGroup;
    // Duration of the fade effect
    public float fadeDuration = 1.5f;

    void Awake()
    {
        // Ensure the Canvas Group is assigned in the Inspector
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    // Public method to start the fade IN (from opaque to transparent)
    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, 1, 0, fadeDuration));
    }

    // Public method to start the fade OUT (from transparent to opaque)
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, 0, 1, fadeDuration));
    }
    
    // Coroutine to handle the fading process
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            // Calculate the current alpha value using Lerp for smooth transition
            float t = (Time.time - startTime) / duration;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null; // Wait until the next frame
        }
        
        cg.alpha = endAlpha; // Ensure the final alpha value is set correctly
    }
}