using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Attack type toggles
    public bool useAoEDamage = true;    
    public bool useDirectAttack = true;

    // AoE damage settings
    public int aoeDamageAmount = 2;
    public float aoeDamageInterval = 0.5f;
    public float aoeRange = 2f;       
    private Coroutine aoeDamageCoroutine;

    // Direct attack settings
    public int attackDamageAmount = 20;  
    public float attackCooldown = 2.5f;       
    public float attackRange = 1.5f;       
    private float attackTimer = 0f;        
    
    private Transform player;              
    private PlayerStats playerStats;      
    private EnemyScript enemyScript;
    
    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
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
        StartCoroutine(enemyScript.AttackAnimation());
        if (playerStats)
        {
            playerStats.TakeDamage(attackDamageAmount);
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
            }

            yield return new WaitForSeconds(aoeDamageInterval);
        }
    }
}
