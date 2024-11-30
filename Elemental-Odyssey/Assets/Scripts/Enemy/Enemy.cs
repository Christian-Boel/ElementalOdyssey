using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackedPossible
{
    public float speed = 2.5f;
    public float detectionRange = 6.5f;
    public float stoppingDistance = 1.5f; // Should match attackRange in EnemyAttack
    public float maxHp = 30;
    public float hp = 30;
    public EnemySpawner spawner;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private List<Color> originalColors = new List<Color>();
    private bool isFlashing = false;
    private Transform player;
    private SpriterAnimationController spriter;
    [SerializeField] private AudioClip hurtSound;

    protected virtual void Start()
    {
        hp = maxHp;
        spriter = GetComponent<SpriterAnimationController>();
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
        spriter.PlayAnimation("WALK");
        foreach (var sr in spriteRenderers)
        {
            originalColors.Add(sr.color);
        }
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.transform;
    }

    protected virtual void Update()
    {
        if (!player)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange && distanceToPlayer > stoppingDistance)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Determine direction
            Vector2 direction = (player.position - transform.position).normalized;

            // Flip the sprite based on direction
            if (direction.x > 0 && transform.localScale.x < 0)
            {
                // Moving right, ensure scale.x is positive
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                // Moving left, ensure scale.x is negative
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public virtual void TakeDmg(float dmg)
    {
        hp -= dmg;
        
        if (!isFlashing)
        {
            SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 0.5f);
            StartCoroutine(FlashRed());
        }

        if (hp <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(TakeDamageAnimation());
    }
    
    IEnumerator TakeDamageAnimation()
    {
        spriter.PlayAnimation("HURT");
        yield return new WaitForSeconds(0.75f);
        ReturnToDefaultAnimation();
    }

    IEnumerator FlashRed()
    {
        isFlashing = true;
        foreach (var sr in spriteRenderers)
        {
            sr.color = new Color(1f, 0f, 0f, sr.color.a); // Red color with original alpha
        }
        
        yield return new WaitForSeconds(0.15f);

        // Revert all sprite colors back to their original colors
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
        isFlashing = false;
    }
    
    public IEnumerator AttackAnimation()
    {
        spriter.PlayAnimation("ATTACK");
        yield return new WaitForSeconds(0.5f);
        ReturnToDefaultAnimation();
    }
    
    void ReturnToDefaultAnimation()
    {
        spriter.PlayAnimation("WALK");
    }
    
    protected void Die()
    {
        Debug.Log("Enemy died");
        spriter.PlayAnimation("DIE");
        StartCoroutine(DespawnObject());
    }

    IEnumerator DespawnObject()
    {
        yield return new WaitForSeconds(1f);
        if (spawner)
        {
            spawner.OnEnemyDeath(this);
        }
        
        Destroy(this.gameObject);
    }
}
