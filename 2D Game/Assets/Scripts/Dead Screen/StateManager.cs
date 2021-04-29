using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [SerializeField] GameObject DeathPanel;
    [SerializeField] GameObject thePlayer;

    public void ReleadSceneFromCampus()
    {
        SceneManager.LoadScene("Campus");
        DeathPanel.SetActive(false);
        thePlayer.SetActive(true);
        thePlayer.GetComponent<PlayerHealthManager>().setMaxHealth();
        thePlayer.GetComponent<PlayerScoreManager>().ScoreReset();
        thePlayer.GetComponent<Player>().transform.position = new Vector2(20, -105);
    }

    public void ChangeSceneByName(string name)
    {

        if (name != null)
        SceneManager.LoadScene(name);

        DeathPanel.SetActive(false);
        thePlayer.SetActive(true);
        thePlayer.GetComponent<PlayerHealthManager>().setMaxHealth();
        thePlayer.GetComponent<PlayerScoreManager>().ScoreReset();
        thePlayer.GetComponent<Player>().transform.position = new Vector2(20, -105);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
