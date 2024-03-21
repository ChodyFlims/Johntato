using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour, IItem
{
    public int healAmount = 1;
    public static event Action<int> OnHealthCollect;

    // Method when collecting a healing item with pass this along to the player health script to heal
    public void Collect()
    {
       OnHealthCollect.Invoke(healAmount);
       SoundEffectManager.Play("PlayerPickup");
       Destroy(gameObject);
    }
}
