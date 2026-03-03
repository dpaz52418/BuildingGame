using System.Collections;
using TMPro;
using UnityEngine;

/* Suggestion: make it so when a player clicks the play button while a line is being typed, the speed of
 * the typing increases. 
 * 
 * Keep in mind this means that the button must not automatically move to the next line of dialog.
*/

/* Secondary suggestion: after completing the first suggestion, make it so that when a player clicks again, the
 * line finishes typing instantly.
 */

public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI textBox; //add dialogue textbox in Inspector
    public float typeDelayInterval = 0.1f;
    Coroutine typingLinesCoroutine;
    public bool isTyping;

    private void Start()
    {
        textBox.text = "";
    }

    public void StartTyping(string line)
    {
        if (isTyping)
        {
            StopTyping();
        }
        typingLinesCoroutine = StartCoroutine(TypingLines(line));
    }

    IEnumerator TypingLines(string line)
    {
        isTyping = true;
        textBox.text = "";

        // convert string into character array
        char[] lineCharArray = line.ToCharArray();

        for (int i=0; i< lineCharArray.Length; i++)
        {
            textBox.text += lineCharArray[i];

            //maybe a sound effect plays per letter typed!

            yield return new WaitForSeconds(typeDelayInterval);
           
        }

        isTyping = false;
    }

    public void StopTyping()
    {
        if (typingLinesCoroutine != null)
        {
            StopCoroutine(typingLinesCoroutine);
            isTyping = false;
        }
    }

    public void DisplayFullText(string line)
    {
        StopTyping();
        textBox.text = line;
    }
}
