using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint; // Reference to the empty GameObject for spawn location
    private List<GameObject> enemies = new List<GameObject>();
    private float spawnTimer = 0f;
    public float spawnInterval = 5f;
    public int maxEnemies = 13;

    [SerializeField] private Vector3 boundarySize = new Vector3(5, 0, 5); // Editable in Inspector

    void Update()
    {
        // Remove any destroyed enemies from the list
        enemies.RemoveAll(enemy => enemy == null);

        // Check if we can spawn a new enemy
        if (enemies.Count < maxEnemies)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f; // Reset spawn timer
            }
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("❌ Spawn Point is not assigned in the Inspector!");
            return;
        }

        Vector3 spawnPos = spawnPoint.position;

        // ✅ Randomize spawn position slightly to avoid stacking
        spawnPos += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

        // ✅ Randomize initial enemy rotation
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        // ✅ Check for nearby enemies to prevent overlapping
        Collider[] colliders = Physics.OverlapSphere(spawnPos, 1.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Debug.LogWarning("⚠ Spawn position occupied! Retrying...");
                return; // Prevents spawning at this position
            }
        }

        // ✅ Spawn the enemy with a random rotation
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, randomRotation);
        enemies.Add(enemy);

        // ✅ Assign movement boundaries for the enemy
        WanderingAI ai = enemy.GetComponent<WanderingAI>();
        if (ai != null)
        {
            ai.boundaryMin = spawnPos - boundarySize;
            ai.boundaryMax = spawnPos + boundarySize;
            ai.SetInitialDirection(); // ✅ Ensure movement starts in a random direction

            Debug.Log($"✅ {enemy.name} Assigned Boundaries: Min {ai.boundaryMin}, Max {ai.boundaryMax}");
        }
    }
}


