using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreChangedHandler(int currentScore);
    public event ScoreChangedHandler OnScoreChanged;
    
    public int CurrentScore
    {
        get;
        private set;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentScore = 0;
        BroadcastScoreChanged();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += AddScore;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= AddScore;
    }

    public void AddScore(int scoreValue)
    {
        if (scoreValue <= 0)
        {
            Debug.LogWarning("The score value must be greater than zero");
            return;
        }
        
        
        CurrentScore += scoreValue;
        BroadcastScoreChanged();
        Debug.Log("Current Score: " + CurrentScore);
    }

    private void BroadcastScoreChanged()
    {
        OnScoreChanged?.Invoke(CurrentScore);
        
    }

}
