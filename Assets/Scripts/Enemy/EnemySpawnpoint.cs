using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public GameObject enemyPrefab; // Default enemy prefab to spawn
    public List<GameObject> alternateEnemyPrefabs = new List<GameObject>(); // List of alternate enemy prefabs
    public int numberOfEnemiesToSpawn = 1; // Number of enemies to spawn

    // Method to call the function that spawns the enemies
    public void OnRestart()
    {
        SpawnEnemies();
    }

    // Spawns the enemies
    void SpawnEnemies()
    {
        // Spawn default enemy if available
        if (enemyPrefab != null)
        {
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }

        // Spawn alternate enemies randomly if available
        if (alternateEnemyPrefabs.Count > 0)
        {
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                int randomIndex = Random.Range(0, alternateEnemyPrefabs.Count);
                Instantiate(alternateEnemyPrefabs[randomIndex], transform.position, Quaternion.identity);
            }
        }
    }
}