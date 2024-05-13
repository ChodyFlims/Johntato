using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class representing a bullet in the game
public class Bullet : MonoBehaviour
{
    public int bulletDamage = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        BossController boss = collision.GetComponent<BossController>();
        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        if (boss)
        {
            boss.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
