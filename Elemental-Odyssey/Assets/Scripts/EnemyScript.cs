using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2.5f;
    public float detectionRange = 6.5f;
    public float stoppingDistance = 1.5f; // Should match attackRange in EnemyAttack
    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.transform;
    }

    void Update()
    {
        if (!player)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > stoppingDistance)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        // Optionally, add idle or patrol behavior when not moving
    }
}