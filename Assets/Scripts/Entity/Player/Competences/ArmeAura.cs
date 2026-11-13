using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmeAura : Arme
{
    private List<EnemyPrefab> enemies = new List<EnemyPrefab>();
    private Cooldown timer;
    public int pourcentageDamage = 10;
    public int pourcentageScaleB = 10;
    private Vector2 scale;
    
    void Start()
    {
        timer = GetComponent<Cooldown>();
        timer.OnTimeout = ArmeAuraAttack;
        scale = transform.localScale;
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

    public override void Amelioration()
    {
        Debug.Log("ArmeAura");
        damage += damage * pourcentageDamage / 100 ;
        float pourcentageScale = 1f + (pourcentageScaleB / 100f);
        Vector2 newScale = transform.localScale * pourcentageScale;
        transform.localScale = newScale;
    }
}
