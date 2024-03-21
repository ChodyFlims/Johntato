using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public GameObject itemPrefab;
    // Chance of dropping the item, ranging from 0 to 100
    [Range(0, 100)] public float dropChance;
}
