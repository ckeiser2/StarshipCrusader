using UnityEngine;
using TMPro;


public class PlayerHealthHUD : MonoBehaviour
{
    [SerializeField]
    private Health playerHealth;

    [SerializeField]
    private TMP_Text healthText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthDisplay;
        
        UpdateHealthDisplay(playerHealth.CurrentHealth, max:playerHealth.MaximumHealth);
    }

    
    private void UpdateHealthDisplay(int current, int max)
    {
        healthText.text = "Health " + current + " / " + max;
    }
    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthDisplay;
        }
    }

}
