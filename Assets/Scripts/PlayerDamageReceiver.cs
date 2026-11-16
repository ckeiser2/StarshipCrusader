using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health playerHealth;
    
    public void TakeDamage(int damageAmount)
    {
        playerHealth.TakeDamage(damageAmount);
    }
}
