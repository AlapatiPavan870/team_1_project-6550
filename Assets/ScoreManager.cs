using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    private int score = 0;
    private bool stopped = false;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore()
    {
        if (!stopped)
        {
            score++;

            if (score > 15)
            {
                stopped = true;
                score = 15; // Cap the score at 15
            }

            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {

        if (stopped)
        {
            scoreText.text = "Score: " + (score / 15f).ToString("0.##") + " / 15";
        }
        else
        {
            scoreText.text = "Score: " + score + " / 15";
        }
    }
}
