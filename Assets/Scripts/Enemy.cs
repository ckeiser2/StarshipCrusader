using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health myHealth;

    public delegate void EnemyDestroyHandler(int sccoreValue);

    public static event EnemyDestroyHandler OnEnemyDestroyed;

    private bool isDefeated = false;
    
    //public enum EnemyType
    //{
    //    Scout = 0,
    //    HeavyFighter = 1,
    //    Turret = 2
    //    
    //}

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

    protected virtual void OnEnable()
    {
        isDefeated = false;
        myHealth.OnHealthChanged += HandleHealthChanged;
        
    }

    protected void OnDisable()
    {
        if (myHealth != null)
        {
            myHealth.OnHealthChanged -= HandleHealthChanged;
        }
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (current > 0 || isDefeated)
        {
            return;
        }

        isDefeated = true;

        OnEnemyDestroyed?.Invoke(enemyData.ScoreValue);
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
