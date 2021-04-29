using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreToPlayer : MonoBehaviour
{
    private PlayerScoreManager scorePlayer;
    [SerializeField]
    public int scoreToGive;
    private bool pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        scorePlayer = FindObjectOfType<PlayerScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerScoreManager>().ScoreIncrease(scoreToGive);
            Destroy(collision.gameObject);
        }
    }
}
