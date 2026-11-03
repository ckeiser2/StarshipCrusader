using UnityEngine;

public class EnemyDataTester : MonoBehaviour
{
    private EnemyData scoutData;
    private EnemyData heavyFighterData;
    
    void Start()
    {
        scoutData = EnemyProfiles.CreateScout();
        
        
        Debug.Log("Enemy: " + scoutData.EnemyName +
                  "| Health: " + scoutData.MaximumHealth +
                  "| Score: " + scoutData.ScoreValue);

        heavyFighterData = EnemyProfiles.CreateHeavyFighter();
        
        Debug.Log("Enemy: " + heavyFighterData.EnemyName +
                  "| Health: " + heavyFighterData.MaximumHealth +
                  "| Score: " + heavyFighterData.ScoreValue );
    }
}
