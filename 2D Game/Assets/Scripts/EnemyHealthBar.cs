using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Text HealthText;
    public BossHealthManager enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = enemyHealth.maxHealth;
        healthBar.value = enemyHealth.currentHealth;
        HealthText.text = "BOSS HP: " + enemyHealth.currentHealth + "/" + enemyHealth.maxHealth;
    }
}
