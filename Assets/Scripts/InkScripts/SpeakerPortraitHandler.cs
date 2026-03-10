using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeakerPortraitHandler : MonoBehaviour
{
    public Sprite portrait_yuriko, portrait_iron;
    public Image portraitImage;
    //public InkDialoguePlayer InkDialogueManager;

    public void UpdatePortrait()
    {
        Tag portraitTag = null;

        portraitTag = InkDialoguePlayer.tags.Find(tag => tag.key == "portrait");

        if (portraitTag != null)
        {
            switch(portraitTag.value)
            {
                case "yuriko":
                    portraitImage.sprite = portrait_yuriko;
                    break;
                case "iron":
                    portraitImage.sprite = portrait_iron;
                    break;
                default:
                    break;
            }
        } else
        {
            Debug.LogError("no portrait.");
        }
    }
}
