using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn = new List<GameObject>(); // List of prefabs to spawn
    public int prefabIndexToSpawn = 0; // Index of the prefab to spawn

    public void SpawnPrefab()
    {
        if (prefabIndexToSpawn < 0 || prefabIndexToSpawn >= prefabsToSpawn.Count)
        {
            Debug.LogError("Prefab index out of range!");
            return;
        }

        GameObject selectedPrefab = prefabsToSpawn[prefabIndexToSpawn];

        if (selectedPrefab != null)
        {
            Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Selected prefab is null!");
        }
    }
}
