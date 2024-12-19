using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IAttackedPossible
{
    public float maxHealth = 1000;
    public float currentHealth;
    public float sceneStartHealth = 1000;
    public float AD = 40; // Attack Damage
    public float MS = 5; // Movement Speed
    public float dashCD = 4; // Dash Cooldown
    public float dashLength = 0.1f; // Dash Length
    public HealthBar healthBar;
    [SerializeField] private AudioClip playerDeathSoundClip;

    public event Action OnStatsChanged;

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("Healthbar")?.GetComponent<HealthBar>();
        if (!healthBar)
        {
            return;
        }
        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        float newHealth = currentHealth + healAmount;
        currentHealth = Mathf.Min(newHealth, maxHealth);
        UpdateHealthBar();
    }

    public void TakeDmg(float dmg)
    {
        currentHealth -= dmg;
        
        UpdateHealthBar();
    
        if (currentHealth <= 0)
        {
            SoundFXManager.instance?.PlaySoundFXClip(playerDeathSoundClip, transform, 1f);
            Die();
        }
        
    }
    
    public void Die()
    {
        currentHealth = sceneStartHealth;
        Debug.Log("Player has died!");
    
        // Genstart den aktuelle scene
        SceneTransitionManager.Instance.SwitchScene(SceneManager.GetActiveScene().name);
    }

    public void NewScene()
    {
        sceneStartHealth = currentHealth;
        while (!healthBar) healthBar = GameObject.FindGameObjectWithTag("Healthbar")?.GetComponent<HealthBar>();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            Debug.Log(currentHealth + maxHealth);
            healthBar.SetHealth(currentHealth, maxHealth);
        }
        updateUI();
    }

    public void updateUI()
    {
        OnStatsChanged?.Invoke();
    }
}
