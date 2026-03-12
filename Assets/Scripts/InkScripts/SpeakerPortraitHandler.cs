using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeakerPortraitHandler : MonoBehaviour
{
    [Header("Portraits")]
    public Image portraitImage;
    public Sprite portrait_adam, portrait_olly, portrait_olly_eve;

    [Header("Backgrounds")]
    [SerializeField] private Image backgroundImage;
    public Sprite background_outside, background_clubhouse, background_melios, background_flames;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    public AudioClip noise_whirring, noise_flames;

    public void UpdatePortrait()
    {
        Tag portraitTag = InkDialoguePlayer.tags.Find(tag => tag.key == "portrait");

        if (portraitTag != null)
        {
            switch (portraitTag.value)
            {
                case "empty":
                    portraitImage.sprite = null;
                    portraitImage.enabled = false;
                    break;
                case "adam":
                    portraitImage.enabled = true;
                    portraitImage.sprite = portrait_adam;
                    break;
                case "olly":
                    portraitImage.enabled = true;
                    portraitImage.sprite = portrait_olly;
                    break;
                case "olly_eve":
                    portraitImage.enabled = true;
                    portraitImage.sprite = portrait_olly_eve;
                    break;
                default:
                    Debug.LogWarning("Unknown portrait tag: " + portraitTag.value);
                    break;
            }
        }
    }

    public void UpdateBackground()
    {
        Tag backgroundTag = InkDialoguePlayer.tags.Find(tag => tag.key == "background");

        if (backgroundTag != null)
        {
            switch (backgroundTag.value)
            {
                case "outside":
                    backgroundImage.sprite = background_outside;
                    break;
                case "clubhouse":
                    Debug.Log("Switching to clubhouse background.");
                    backgroundImage.sprite = background_clubhouse;
                    break;
                case "melios":
                    backgroundImage.sprite = background_melios;
                    break;
                case "flames":
                    backgroundImage.sprite = background_flames;
                    break;
                default:
                    Debug.LogWarning("Unknown background tag: " + backgroundTag.value);
                    break;
            }
        }
    }

    public void UpdateNoise()
    {
        Tag noiseTag = InkDialoguePlayer.tags.Find(tag => tag.key == "noise");

        if (noiseTag != null)
        {
            switch (noiseTag.value)
            {
                case "whirring":
                    audioSource.clip = noise_whirring;
                    audioSource.Play();
                    break;
                case "flames":
                    audioSource.clip = noise_flames;
                    audioSource.Play();
                    break;
                default:
                    Debug.LogWarning("Unknown noise tag: " + noiseTag.value);
                    break;
            }
        }
    }
}
