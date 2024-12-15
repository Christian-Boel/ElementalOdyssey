using TMPro;
using UnityEngine;

public class LockedGate : MonoBehaviour
{
    public EnemySpawner linkedSpawner;  // Reference to the enemy spawner
    public GameObject textBox;           // Optional text box for player feedback
    private TextMeshProUGUI textComp;
    private bool isUnlocked = false;

    private void Start()
    {
        if (textBox != null)
        {
            textComp = textBox.GetComponent<TextMeshProUGUI>();
            textComp.text = "Defeat all enemies to proceed!";
            textBox.SetActive(false);
        }

        if (linkedSpawner == null)
        {
            Debug.LogError("LockedGate: No EnemySpawner assigned!");
        }
    }

    private void Update()
    {
        if (!isUnlocked && linkedSpawner != null)
        {
            if (linkedSpawner.allEnemiesDefeated)
            {
                UnlockGate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isUnlocked && textBox != null)
        {
            textBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && textBox != null)
        {
            textBox.SetActive(false);
        }
    }

    private void UnlockGate()
    {
        isUnlocked = true;
        Debug.Log("Gate unlocked!");
        gameObject.SetActive(false);
    }
}