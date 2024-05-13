using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BossController : MonoBehaviour
{
    public int maxHealth = 12; // Maximum health of the boss
    private int currentHealth; // Current health of the boss
    private Transform player;
    public float moveSpeed = 0.5f; // Speed at which the boss moves
    public Transform startPoint; // Starting point of the boss movement
    public Transform endPoint; // Ending point of the boss movement
    public GameObject bulletPrefab; // Prefab of the bullet to shoot
    public float shootInterval = 2f; // Interval between shots

    public float bulletLifetime = 2f; // Lifetime of bullets
    public float bulletSpeed = 50f; // Speed of the bullets
    public int damage = 1;
    private float trackPercent = 0f; // Percentage of movement along the track
    private int direction = 1; // Movement direction (1 for forward, -1 for backward)
    private float lastShootTime; // Time when the boss last shot
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Color ogColor;

    private bool bossIsFacingLeft = false;

    public GameObject winConditionObject;
    public Vector3 winConditionSpawnPoint;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get reference to SpriteRenderer component
        ogColor = spriteRenderer.color;
    }

    void Update()
    {
        if (startPoint == null || endPoint == null)
            return;

        // Move the boss along the track
        trackPercent += direction * moveSpeed * Time.deltaTime;
        float x = Mathf.Lerp(startPoint.position.x, endPoint.position.x, trackPercent);
        float y = Mathf.Lerp(startPoint.position.y, endPoint.position.y, trackPercent);
        transform.position = new Vector3(x, y, transform.position.z);

        // Flip sprite if moving left
        if (direction == -1)
        {
            spriteRenderer.flipX = true;
            bossIsFacingLeft = false;
        }
        else
        {
            spriteRenderer.flipX = false;
            bossIsFacingLeft = true;
        }

        // Reverse direction at start/finish positions
        if ((direction == 1 && trackPercent > 0.9f) || (direction == -1 && trackPercent < 0.1f))
        {
            direction *= -1;
        }

        // Check if it's time to shoot
        if (Time.time - lastShootTime >= shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        Vector3 shootDirection = bossIsFacingLeft ? -transform.right : transform.right; // Shoot left if bossIsFacingLeft, otherwise shoot right

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        SoundEffectManager.Play("EnemyShoot");

        // Fetch the SpriteRenderer component from the bullet GameObject
        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        if (bulletSpriteRenderer != null && bossIsFacingLeft == false)
        {
            // Flip the bullet sprite if the boss is facing right
            bulletSpriteRenderer.flipX = true;
        }

        if (bulletRb != null)
        {
            bulletRb.velocity = shootDirection * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashRed());
        if(currentHealth <= 0)
        {
            SoundEffectManager.Play("EnemyDeath");
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = ogColor;
    }

    public void Die()
    {   
        Vector3 winConditionSpawnPoint = new Vector3(2.11f,-2.39f,0f);
        // Instantiate the WinCondition GameObject at the specified position when the boss dies
        if (winConditionObject != null)
        {
            Instantiate(winConditionObject, winConditionSpawnPoint, Quaternion.identity);
        }
        // Destroy the boss
        Destroy(gameObject);
    }
}