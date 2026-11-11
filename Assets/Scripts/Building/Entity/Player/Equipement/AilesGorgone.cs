using System;
using System.Collections.Generic;
using UnityEngine;

public class AilesGorgone : Equipement
{
    [Range(30f, 40f)] public float negateChance;
    [SerializeField] [Range(1f, 200f)] private float knockbackValue;
    private List<Rigidbody2D> enemies = new List<Rigidbody2D>();
    public override void OnPickUp(Controlleur player) {}

    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    public override void EquipementAction()
    {
        foreach (Rigidbody2D en in enemies)
        {
            Vector2 force = (en.transform.position - transform.position).normalized * knockbackValue;
            if (en.linearDamping == 0)
            {
                en.linearDamping = 10;
            }
            en.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
