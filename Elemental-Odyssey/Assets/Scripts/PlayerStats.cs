using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IAttackedPossible
{
    public float maxHealth = 1000;
    public float currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>();
        UpdateHealthBar();
    }
    
    public void Heal(float healAmount)
    {
        float newHealth = currentHealth + healAmount;
        currentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        UpdateHealthBar();
    }
    
    public void TakeDmg(float dmg)
    {
        currentHealth -= dmg;
        if (healthBar)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
        else
        {
            Debug.Log("Healthbar not found");
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died!");
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }
}
