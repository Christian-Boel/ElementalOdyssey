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

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // List to keep track of spawned enemies
    private Transform player;
    private bool playerInRange = false;
    private Coroutine spawnCoroutine;
    private Coroutine despawnCoroutine;

    void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject)
            player = playerObject.transform;
        else
            Debug.LogError("Player not found in the scene.");

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

            // Start spawning enemies if not already started
            if (spawnCoroutine == null)
            {
                spawnCoroutine = StartCoroutine(SpawnEnemies());
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
            if (spawnedEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 2f; // Random position around spawner
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemies.Add(enemy);
    }

    IEnumerator DespawnEnemiesAfterDelay()
    {
        yield return new WaitForSeconds(despawnDelay);

        // Destroy all spawned enemies
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy)
            {
                Destroy(enemy);
            }
        }

        spawnedEnemies.Clear();
    }
}
