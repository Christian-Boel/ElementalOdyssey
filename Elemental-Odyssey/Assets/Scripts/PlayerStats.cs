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
        OnStatsChanged?.Invoke();
    }

    public void SaveStats()
    {
        // Gem stats i PlayerPrefs
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        PlayerPrefs.SetFloat("MaxHealth", maxHealth);
        PlayerPrefs.SetFloat("AttackDamage", AD);
        PlayerPrefs.SetFloat("MovementSpeed", MS);
        PlayerPrefs.SetFloat("DashCooldown", dashCD);
        PlayerPrefs.SetFloat("DashLength", dashLength);
        UpdateHealthBar();
    }

    public void RestoreStats()
    {
        // Gendan stats fra PlayerPrefs
        currentHealth = PlayerPrefs.GetFloat("PlayerHealth", maxHealth);
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", maxHealth);
        AD = PlayerPrefs.GetFloat("AttackDamage", AD);
        MS = PlayerPrefs.GetFloat("MovementSpeed", MS);
        dashCD = PlayerPrefs.GetFloat("DashCooldown", dashCD);
        dashLength = PlayerPrefs.GetFloat("DashLength", dashLength);
        UpdateHealthBar();
    }
}
