using System;
using UnityEngine;

public class Arrow : BasicBullet
{
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    public virtual void Init(GameObject _target, float _damage, float _speed)
    {
        target = _target;
        damage = _damage;
        speed = _speed;
        
        direction = target.transform.position - transform.position;
        direction.Normalize();
        print(target.transform.position + " " + transform.position + " " + direction);
        var targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = targetRotation;
        print("spawned");
        Destroy(gameObject,10f);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(target.tag))
        {
            print("uhm??? damage");
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Controlleur>().currentHealth -= damage;
            } 
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyPrefab>().TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }
}
