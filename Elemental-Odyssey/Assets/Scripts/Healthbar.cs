using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image greenBarImage; // Reference to the green bar Image component
    public float maxHealth = 1000;

    private float originalWidth; // Original width of the green bar
    private RectTransform greenBarRect;

    void Start()
    {
        if (greenBarImage != null)
        {
            greenBarRect = greenBarImage.GetComponent<RectTransform>();
            // Store the original width of the green bar
            originalWidth = greenBarRect.rect.width;
            
        }
        else
        {
            Debug.LogError("Green Bar Image is not assigned.");
        }
    }
    
    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (greenBarRect != null)
        {
            // Calculate the percentage of health remaining
            float healthPercent = currentHealth / maxHealth;

            // Ensure healthPercent is between 0 and 1
            healthPercent = Mathf.Clamp01(healthPercent);

            // Adjust the width of the green bar
            greenBarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWidth * healthPercent);
        }
        else
        {
            Debug.LogError("Green Bar RectTransform is not assigned.");
        }
    }
}