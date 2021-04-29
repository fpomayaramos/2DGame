using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStateManager : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject thePlayer;

    public void ReleadSceneFromCampus()
    {
        WinPanel.SetActive(false);
        SceneManager.LoadScene("Campus");
        WinPanel.SetActive(false);
        thePlayer.SetActive(true);
        thePlayer.GetComponent<PlayerHealthManager>().setMaxHealth();
        thePlayer.GetComponent<PlayerScoreManager>().ScoreReset();
        thePlayer.GetComponent<Player>().transform.position = new Vector2(20, -105);
    }

    public void ChangeSceneByName(string name)
    {

        if (name != null)
            SceneManager.LoadScene(name);

        WinPanel.SetActive(false);
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
