using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IAttackedPossible
{
    public float maxHealth = 1000;
    public float currentHealth;
    public float AD = 40;
    public float MS = 5;
    public float dashCD = 4;
    public float dashLength = .1f;
    public HealthBar healthBar;
    public GameObject player;
    [SerializeField] private AudioClip playerDeathSoundClip;

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
        Debug.Log("dfsdf");
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
            SoundFXManager.instance.PlaySoundFXClip(playerDeathSoundClip, transform, 1f);
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
