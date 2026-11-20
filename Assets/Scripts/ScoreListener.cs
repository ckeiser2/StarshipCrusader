using UnityEngine;

public class ScoreListener : MonoBehaviour
{
    
    [SerializeField]
    private ScorePublisher _scorePublisher;

    //private ScorePublisher.ScoreAwardedHandler _myScoreAwardedHandler;

    private int currentScore = 0;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _scorePublisher.OnScoreAwarded += AddScore;
        //_scorePublisher.OnScoreAwarded += _myScoreAwardedHandler;
    }

    private void OnDisable()
    {
        _scorePublisher.OnScoreAwarded -= AddScore;
        //_scorePublisher.OnScoreAwarded += _myScoreAwardedHandler;
    }

    private void AddScore(int scoreValue)
    {
        currentScore += scoreValue;
        
        Debug.Log(gameObject.name + ": Current Score: "+ currentScore);
    }
}
