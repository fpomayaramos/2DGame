using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score <= 0)
        {
            score = 0;
        }
    }

    public void ScoreReset()
    {
        score = 0;
    }

    public void ScoreIncrease(int scoreToGive)
    {
        score += scoreToGive;
    }
}
