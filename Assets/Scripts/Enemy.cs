using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health myHealth;

    
    public enum EnemyType
    {
        Scout = 0,
        HeavyFighter = 1,
        Turret = 2
        
    }

    public EnemyData enemyData
    {
        get;
        private set;
        
    }
    
    protected virtual void Start()
    {
        InitializeEnemy();
    }

    
    protected void Update()
    {
        UpdateBehavior();
    }

    protected virtual void UpdateBehavior()
    {
        
    }

    protected void InitializeEnemy()
    {
        enemyData = CreateEnemyData();

        myHealth.Initialize(enemyData.MaximumHealth);
        
        LogEnemyData();
    }

    protected virtual EnemyData CreateEnemyData()
    {
        return EnemyProfiles.CreateScout();
    }
    
    private void LogEnemyData()
    {
        Debug.Log("Created " + enemyData.EnemyName + 
                  "| Health" + enemyData.MaximumHealth + 
                  "| Score:" + enemyData.ScoreValue);
    }
    
    public void TakeDamage(int damageAmount)
    {
        myHealth.TakeDamage(damageAmount);
    }
}
