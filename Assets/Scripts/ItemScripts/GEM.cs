using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class representing a gem item
public class GEM : MonoBehaviour, IItem
{
    public static event Action<int> OnGemCollect;
    public int worth = 100;

    // When collecting a gem invoke a event to send to get tallied up
    public void Collect()
    {
        OnGemCollect.Invoke(worth);
        SoundEffectManager.Play("Gempickup");
        Destroy(gameObject);
    }
}
