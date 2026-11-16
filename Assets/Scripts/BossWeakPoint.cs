using UnityEngine;

public class BossWeakPoint : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health weakPointHealth;

    [Min(1)]
    [SerializeField]
    private int maximumHealth = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weakPointHealth.Initialize(maximumHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        weakPointHealth.TakeDamage(damageAmount);
    }


}
