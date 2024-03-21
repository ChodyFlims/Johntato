using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class representing a double jump item
public class DoubleJump : MonoBehaviour, IItem
{
    public bool doubleJumpEnabled = true;

    public static event Action<bool> OnDoubleJumpCollect;

    // When collecting the item then invoke a event to enable double jumping
    public void Collect()
    {
        OnDoubleJumpCollect.Invoke(doubleJumpEnabled);
        SoundEffectManager.Play("PlayerPickup");
        Destroy(gameObject);
    }
}
