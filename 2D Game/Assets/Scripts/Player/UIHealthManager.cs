using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthManager : MonoBehaviour
{
    public Slider healthBar;
    public Text HealthText;
    public PlayerHealthManager playerHealth;

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

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.currentHealth;
        HealthText.text = "HP: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth;
    }
}
