using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")] 
    
    [Min(1)]
    [SerializeField]
    private int maximumHealth = 3;

    public int CurrentHealth {
        get
        {
            return currentHealth;
        }
    }

    public int MaximumHealth
    {
        get
        {
            return maximumHealth;
        }
    }
    
    private int currentHealth;
    
    

    private bool isDefeated;

    public delegate void HealthChangedHandler(int current, int max);

    public event HealthChangedHandler OnHealthChanged;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Initialize(maximumHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BroadcastHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, max:maximumHealth);
    }

    public void Initialize(int startingMaxHealth)
    {
        maximumHealth = Mathf.Max(startingMaxHealth, 1);
        currentHealth = maximumHealth;
        isDefeated = false;

        BroadcastHealthChanged();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDefeated == true)
        {
            return;
        }
        if (damageAmount <= 0)
        {
            return;
        }

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        BroadcastHealthChanged();

        if (currentHealth == 0)
        {
            Defeat();
        }
        
        Debug.Log(gameObject.name + ": Took Damage amount " + damageAmount + ". " + currentHealth + "/" + maximumHealth);    
        
    }

    private void Defeat()
    {
        isDefeated = true;
        
        Debug.Log(gameObject.name + " was defeated ");
        
        Destroy(gameObject);
    }
    

}
