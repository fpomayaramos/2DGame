using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
    }

    public void setMaxHealth()
    {
        currentHealth = maxHealth;
    }
}
