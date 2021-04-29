using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelManager : MonoBehaviour
{
    public static WinLevelManager instance;

    private static bool UIExists;

    // Start is called before the first frame update
    void Start()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyChildOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // goes up the parent chain and sets the DontDestroyOnLoad flag on the root object.
    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        // If this object doesn't have a parent then its the root transform.
        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }
        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
    }

    private void Awake()
    {
        if (WinLevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void WinGameOver()
    {
        UIWinManager _ui = GetComponent<UIWinManager>();
        if (_ui != null)
        {
            _ui.ToggleWinPanel();
        }
    }
}
