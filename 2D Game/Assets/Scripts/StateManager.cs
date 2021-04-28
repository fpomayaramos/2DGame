using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject thePlayer;

    public void ReleadSceneFromCampus()
    {
        SceneManager.LoadScene("Campus");
        deathPanel.SetActive(false);
        thePlayer.SetActive(true);
        thePlayer.GetComponent<PlayerHealthManager>().setMaxHealth();

    }

    public void ChangeSceneByName(string name)
    {

        if (name != null)
        SceneManager.LoadScene(name);

        deathPanel.SetActive(false);
        thePlayer.SetActive(true);
        thePlayer.GetComponent<PlayerHealthManager>().setMaxHealth();
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
