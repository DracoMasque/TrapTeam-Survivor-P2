using System.Collections.Generic;
using UnityEngine;

public class Mamouth : Boss
{
    
    [SerializeField] [Range(1f, 200f)] private float knockbackValue;
    private List<EnemyPrefab> enemies = new List<EnemyPrefab>();
   
    void Start()
    {
       base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject.GetComponent<EnemyPrefab>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject.GetComponent<EnemyPrefab>());
        }
    }
    
    private void HitEnnemies()
    {
        if (enemies.Count > 0)
        {
            foreach (EnemyPrefab enemy in enemies)
            {
                enemy.TakeDamageTrap(attack, elementBoss.elements, bossRoom.bonusElement);
                Vector2 force = (enemy.transform.position - transform.position).normalized * knockbackValue;
                enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
    
}
