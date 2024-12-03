using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public TextMeshProUGUI playerStatsText; // Reference to the UI Text element for displaying stats
    private PlayerStats playerStats; // Reference to the PlayerStats script

    void Start()
    {
        playerStats = GameManager.Instance.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.OnStatsChanged += UpdateStatsDisplay;
            UpdateStatsDisplay();
        }
        else
        {
            Debug.LogError("PlayerStats component not found on GameManager.");
        }
    }
    
    void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.OnStatsChanged -= UpdateStatsDisplay;
        }
    }
    
    private void UpdateStatsDisplay()
    {
        playerStatsText.text = 
            $"Max Health: {playerStats.maxHealth}\n" +
            $"Attack Damage: {playerStats.AD}\n" +
            $"Movement Speed: {playerStats.MS}\n" +
            $"Dash Cooldown: {playerStats.dashCD}\n" +
            $"Dash Length: {playerStats.dashLength}";
    }
} 