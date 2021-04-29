using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    [SerializeField]
    public int scoreToGive = 100;

    private PlayerScoreManager playerScore;

    private bool flashActive;
    [SerializeField]
    private float flashLenght = 0.5f;
    private float flashCounter = 0f;
    private SpriteRenderer enemySprite;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        enemySprite = GetComponent<SpriteRenderer>();
        playerScore = FindObjectOfType<PlayerScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashActive)
        {
            // 4 colors
            if (flashCounter > flashLenght * 0.99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLenght * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLenght * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLenght * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLenght * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLenght * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }

    public void BossDied()
    {
        WinLevelManager.instance.WinGameOver();
    }

    public void HurtEnemy(int damageToGive)
    {
        currentHealth -= damageToGive;

        flashActive = true;
        flashCounter = flashLenght;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            playerScore.ScoreIncrease(scoreToGive);
            BossDied();
            // Destroy enemy
            gameObject.SetActive(false);
        }
    }

    public void setMaxHealth()
    {
        currentHealth = maxHealth;
    }
}
