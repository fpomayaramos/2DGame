using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePicker : MonoBehaviour
{
    [SerializeField]
    public int scoreToGive = 100;
    private PlayerScoreManager playerScore;
    private PlayerHealthManager playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = FindObjectOfType<PlayerScoreManager>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Heal")
        {
            playerScore.ScoreIncrease(scoreToGive);
            playerHealth.currentHealth += 10;
            if (playerHealth.currentHealth > playerHealth.maxHealth)
                playerHealth.currentHealth = playerHealth.maxHealth;
            Destroy(collision.gameObject);
        }
    }
}
