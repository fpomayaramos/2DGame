using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This class change from one scene to another
 * Clicking on a button in a current scene will 
 * redirect the user to the next scene (String sceneName)
 */
public class ChangeScene : MonoBehaviour
{
    
    public void buttonChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
