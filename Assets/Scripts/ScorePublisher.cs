using UnityEngine;

public class ScorePublisher : MonoBehaviour
{
    public delegate void ScoreAwardedHandler(int scoreValue);

    public event ScoreAwardedHandler OnScoreAwarded;

    [Min(1)] [SerializeField] private int testScoreValue = 100;

    private void Start()
    {
        PublishScoreAwarded();
    }
    
    
    
    private void PublishScoreAwarded()
    {
        Debug.Log("The publisher is announced a score award.");

        OnScoreAwarded?.Invoke(testScoreValue);
        
    }



}
