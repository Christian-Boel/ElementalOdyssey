using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Attack type toggles
    public bool useAoEDamage = true;    // Enable or disable AoE damage
    public bool useDirectAttack = true; // Enable or disable direct attacks

    // AoE damage settings
    public int aoeDamageAmount = 2;        // Damage per AoE tick
    public float aoeDamageInterval = 0.5f;    // Time between AoE damage ticks
    public float aoeRange = 2f;             // Radius for AoE damage
    private Coroutine aoeDamageCoroutine;   // Reference to the AoE damage coroutine

    // Direct attack settings
    public int attackDamageAmount = 20;     // Damage per direct attack
    public float attackCooldown = 2.5f;       // Time between direct attacks
    public float attackRange = 1.5f;        // Range for direct attacks
    private float attackTimer = 0f;         // Timer to track attack cooldown

    // References
    private Transform player;               // Player's transform
    private PlayerStats playerStats;        // Player's stats script

    void Start()
    {
        // Find the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerStats = playerObject.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        if (!player)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Handle AoE damage
        if (useAoEDamage && distanceToPlayer <= aoeRange)
        {
            if (aoeDamageCoroutine == null)
            {
                aoeDamageCoroutine = StartCoroutine(DamagePlayerAoE());
            }
        }
        else
        {
            if (aoeDamageCoroutine != null)
            {
                StopCoroutine(aoeDamageCoroutine);
                aoeDamageCoroutine = null;
            }
        }

        // Handle direct attacks
        if (useDirectAttack && distanceToPlayer <= attackRange)
        {
            if (attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown; // Reset cooldown
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    void AttackPlayer()
    {
        if (playerStats)
        {
            playerStats.TakeDamage(attackDamageAmount);
            Debug.Log("Enemy performs a direct attack!");
        }
    }

    IEnumerator DamagePlayerAoE()
    {
        // Initial delay if necessary
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            if (playerStats)
            {
                playerStats.TakeDamage(aoeDamageAmount);
                Debug.Log("Enemy deals AoE damage!");
            }

            yield return new WaitForSeconds(aoeDamageInterval);
        }
    }
}
