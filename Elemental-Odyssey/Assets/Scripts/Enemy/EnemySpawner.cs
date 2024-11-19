using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;      // The enemy prefab to spawn
    public int maxEnemies = 5;          // Maximum number of enemies to spawn at once
    public float spawnRadius = 15f;     // Radius within which the spawner activates
    public float spawnInterval = 2f;    // Time interval between enemy spawns
    public float despawnDelay = 5f;     // Delay before despawning enemies after player exits radius
    public bool shouldRespawn = true;   // Whether the spawner should respawn enemies
    public float respawnDelay = 5f;     // Delay between enemy death and respawn

    private List<Enemy> spawnedEnemies = new List<Enemy>(); // List to keep track of spawned enemies
    private Transform player;
    private bool playerInRange = false;
    private Coroutine spawnCoroutine;
    private Coroutine despawnCoroutine;
    private int pendingRespawns = 0;    // Count of enemies scheduled to respawn
    private bool hasSpawnedInitialEnemies = false; // Tracks if initial enemies have been spawned

    void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject)
        {
            player = playerObject.transform;
        }
        else
        { 
            Debug.LogError("Player not found in the scene.");
        }

        // Ensure the collider matches the spawn radius
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider)
        {
            collider.isTrigger = true;
            collider.radius = spawnRadius;
        }
        else
        {
            Debug.LogError("CircleCollider2D not found on EnemySpawner.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Stop any ongoing despawn coroutine
            if (despawnCoroutine != null)
            {
                StopCoroutine(despawnCoroutine);
                despawnCoroutine = null;
            }

            if (shouldRespawn)
            {
                // Start spawning enemies if not already started
                if (spawnCoroutine == null)
                {
                    spawnCoroutine = StartCoroutine(SpawnEnemies());
                }
            }
            else
            {
                // Spawn initial enemies only once
                if (!hasSpawnedInitialEnemies)
                {
                    hasSpawnedInitialEnemies = true;
                    // Spawn initial enemies up to maxEnemies
                    for (int i = 0; i < maxEnemies; i++)
                    {
                        SpawnEnemy();
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Stop spawning enemies
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }

            // Start despawning enemies after a delay
            if (despawnCoroutine == null)
            {
                despawnCoroutine = StartCoroutine(DespawnEnemiesAfterDelay());
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (playerInRange)
        {
            // Clean up any null references (enemies that have been destroyed)
            spawnedEnemies.RemoveAll(item => item == null);

            // Only spawn new enemies if there's room and no pending respawns
            if (spawnedEnemies.Count + pendingRespawns < maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 2f; // Random position around spawner
        GameObject enemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.spawner = this;
            spawnedEnemies.Add(enemy);
        }
        else
        {
            Debug.LogError("Spawned enemy does not have an Enemy script attached.");
        }
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        spawnedEnemies.Remove(enemy);

        if (shouldRespawn && playerInRange)
        {
            pendingRespawns++;
            StartCoroutine(RespawnEnemyAfterDelay(respawnDelay));
        }
    }

    IEnumerator RespawnEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        pendingRespawns--;

        if (playerInRange && spawnedEnemies.Count + pendingRespawns < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    IEnumerator DespawnEnemiesAfterDelay()
    {
        yield return new WaitForSeconds(despawnDelay);

        // Destroy all spawned enemies
        foreach (Enemy enemy in spawnedEnemies)
        {
            if (enemy)
            {
                Destroy(enemy.gameObject);
            }
        }

        spawnedEnemies.Clear();
        pendingRespawns = 0; // Reset pending respawns when despawning
    }
}
