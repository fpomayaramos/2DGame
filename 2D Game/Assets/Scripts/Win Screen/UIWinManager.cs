using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinManager : MonoBehaviour
{
    [SerializeField] GameObject winPanel;

    public void ToggleWinPanel()
    {
        winPanel.SetActive(true);
    }
}
