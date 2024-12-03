using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats; // Reference to the PlayerStats script
    public Text playerStatsText; // Reference to the UI Text element

    void Update()
    {
        UpdateStatsDisplay();
    }

    private void UpdateStatsDisplay()
    {
        playerStatsText.text = $"Health: {playerStats.currentHealth}/{playerStats.maxHealth}";
    }
} 