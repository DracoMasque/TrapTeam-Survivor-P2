using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmeAura : Arme
{
    private List<EnemyPrefab> enemies = new List<EnemyPrefab>();
    private Cooldown timer;
    
    void Start()
    {
        timer = GetComponent<Cooldown>();
        timer.OnTimeout = ArmeAuraAttack;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject.GetComponent<EnemyPrefab>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject.GetComponent<EnemyPrefab>());
        }
    }
    void ArmeAuraAttack()
    {
        if (enemies.Count == 0) return;
        foreach (EnemyPrefab enemy in enemies)
        {
            enemy.TakeDamage(damage);
        }
    }
}
