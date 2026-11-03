using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Health myHealth;
    
    [SerializeField] 
    private EnemyType enemyType;
    
    public enum EnemyType
    {
        Scout = 0,
        HeavyFighter = 1
        
    }

    public EnemyData enemyData
    {
        get;
        private set;
        
    }
    
    void Start()
    {
        enemyData = CreateEnemyData(enemyType);

        myHealth.Initialize(enemyData.MaximumHealth);

        gameObject.name = enemyData.EnemyName;
    }

    
    void Update()
    {
        
    }

    private EnemyData CreateEnemyData(EnemyType selectedType)
    {
        if (enemyType == EnemyType.Scout)
        {
            return EnemyProfiles.CreateScout();
        }

        return EnemyProfiles.CreateHeavyFighter();
        
    }
}
