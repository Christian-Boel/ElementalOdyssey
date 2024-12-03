using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public TextMeshProUGUI playerStatsText; // Reference to the UI Text element for displaying stats
    private PlayerStats playerStats; // Reference to the PlayerStats script

    void Update()
    {
        if (!playerStats)
        {
            playerStats = GameManager.Instance.GetComponent<PlayerStats>(); 
        }
        UpdateStatsDisplay();
    }
    
    
    private void UpdateStatsDisplay()
    {
        playerStatsText.text = 
            $"Health: {playerStats.currentHealth}/{playerStats.maxHealth}\n" +
            $"Attack Damage: {playerStats.AD}\n" +
            $"Movement Speed: {playerStats.MS}\n" +
            $"Dash Cooldown: {playerStats.dashCD}\n" +
            $"Dash Length: {playerStats.dashLength}";
    }
} 