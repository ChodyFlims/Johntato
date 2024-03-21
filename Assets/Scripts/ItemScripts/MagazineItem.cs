using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineItem : MonoBehaviour, IItem
{
    public int ReloadBullets = 6;
    public static event Action<int> OnBulletCollect;

    // Method the handle collecting of a magazine will trigger an event to reload the ammo in the gun to max
    public void Collect()
    {
       OnBulletCollect.Invoke(ReloadBullets);
       SoundEffectManager.Play("PlayerPickup");
       Destroy(gameObject);
    }
}
