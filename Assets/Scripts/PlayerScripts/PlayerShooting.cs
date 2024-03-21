using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public AmmoCountUI ammoUI;
    public Animator animator;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public int maxShots = 6;
    public int shotsFired = 6;

    public int empty = 0;

    // Subscribe to events and initialize UI

    void Start()
    {
        MagazineItem.OnBulletCollect += Reload;
        ammoUI.SetMaxBullets(maxShots);
    }

    // Update is called once per frame
    // Check for shooting input
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && shotsFired > empty) // Left Click
        {
            animator.SetBool("isShooting", true);
            Shoot();
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }

    // Execute the shooting
    void Shoot()
    {
        Vector3 shootDirection = transform.right;

        if (!IsFacingRight())
        {
            shootDirection *= -1f;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        Destroy(bullet, 2f);
        
        SoundEffectManager.Play("PlayerShoot");
        shotsFired--;
        ammoUI.UpdateBullets(shotsFired);
    }

    // Method to reload bullets when picking up ammo
    void Reload(int amount)
    {
        if (shotsFired <= maxShots)
        {
            shotsFired = amount;
            ammoUI.UpdateBullets(shotsFired);
            SoundEffectManager.Play("PlayerReload");
        }
    }

    // Method to check if the character is facing right
    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}