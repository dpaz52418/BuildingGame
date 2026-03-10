using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        // the scene name is defined in the editor.
        SceneManager.LoadScene(sceneName);
    }
}
