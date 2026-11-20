using UnityEngine;
using TMPro;
using System;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;
    
    [SerializeField]
    private TMP_Text scoreText;

    private void OnEnable()
    {
        scoreManager.OnScoreChanged += UpdateScoreDisplay;

        UpdateScoreDisplay(scoreManager.CurrentScore);
        

    }

    private void OnDisable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged -= UpdateScoreDisplay;
        }
    }

    private void UpdateScoreDisplay(int currentScore)
    {
        scoreText.text = "Score: " + currentScore;
    }
}
