using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Max health of player
    public int maxHealth = 3;
    private int currentHealth;

    // Takes in the UI elements
    public HealthUI healthUI;

    private SpriteRenderer spriteRenderer;

    public static event Action OnPlayedDied;

    // Start is called before the first frame update
    // Inititalize health and subscribe to events
    void Start()
    {
        ResetHealth();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameController.OnReset += ResetHealth;
        HealthItem.OnHealthCollect += Heal;
    }

    // When the player touches a trap or an enemy then take damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
        
        Trap trap = collision.GetComponent<Trap>();
        if(trap && trap.damage > 0)
        {
            TakeDamage(trap.damage);
        }
    }

    // Healing the player when they grab a health item
    void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthUI.UpdateHearts(currentHealth);
    }

    // Resetting the players health back to full
    void ResetHealth()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    // When the player takes damage
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        StartCoroutine(FlashRed());
        SoundEffectManager.Play("PlayerDamage");

        if(currentHealth <= 0)
        {
            SoundEffectManager.Play("PlayerDeath");
            OnPlayedDied.Invoke();
        }
    }

    // Flash red when taking damage
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}