using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Attack type toggles
    public bool useAoEDamage = true;    
    public bool useDirectAttack = true;

    // AoE damage settingss
    public int aoeDamageAmount = 2;
    public float aoeDamageInterval = 0.5f;
    public float aoeRange = 2f;       
    private Coroutine aoeDamageCoroutine;

    // Direct attack settings
    public int attackDamageAmount = 20;  
    public float attackCooldown = 2.5f;       
    public float attackRange = 1.5f;       
    private float attackTimer = 0f;        
    
    public Transform player;              
    public PlayerStats playerStats;      
    private Enemy _enemy;
    
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        findPlayer();
    }

    private void findPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (!playerObject)
        {
            Debug.LogError("Player not found");
            return;
        }
        Debug.Log("Player found");
        player = playerObject.transform;
        playerStats = playerObject.GetComponentInParent<PlayerStats>();
        if (!player)
        {
            Debug.LogError("NO PLAYER FOUND");
        }
        if (!playerStats)
        {
            Debug.LogError("NO PLAYER STATS FOUND");
        }
    }

    void Update()
    {
        if (!player)
        {   
            findPlayer();
        }

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
        if (_enemy.hp <= 0)
        {
            return;
        }
        Debug.Log("Enemy - attack player");
        StartCoroutine(_enemy.AttackAnimation());
        
        if (!playerStats)
        {
            Debug.LogError("Enemy - could not find playerstats!");
            return;
        }
        Debug.Log("Enemy performs a direct attack!");
        playerStats.TakeDmg(attackDamageAmount);
    }

    IEnumerator DamagePlayerAoE()
    {
        // Initial delay if necessary
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            if (playerStats)
            {
                playerStats.TakeDmg(aoeDamageAmount);
                Debug.Log("Enemy deals AoE damage!");
            }

            yield return new WaitForSeconds(aoeDamageInterval);
        }
    }
}
