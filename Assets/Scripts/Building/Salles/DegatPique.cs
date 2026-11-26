using System;
using System.Collections.Generic;
using UnityEngine;

public class DegatPique : MonoBehaviour
{
    public  int degatPique;
    public Sprite[] sprites;
    List<GameObject> enemies = new List<GameObject>();
    private SpriteRenderer selfSprite;

    void Start()
    {
        selfSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.tag == "Player")
        // {
        //     collision.gameObject.GetComponent<Controlleur>().currentHealth -= degatPique;
        //     
        // }

        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);
        }
    }
    
    public void Hurt()
    {
        print("hurt");
        if (enemies.Count <= 0) return;
        foreach (GameObject enemy in enemies)
        {
            Room parentRoom = GetComponentInParent<Room>();
            enemy.GetComponent<EnemyPrefab>().TakeDamageTrap(degatPique, parentRoom.elements.elements, parentRoom.bonusElement);
        }
    }

    void zero()
    {
        selfSprite.sprite = sprites[4];
    }
    void one()
    {
        selfSprite.sprite = sprites[0];
    }

    void two()
    {
        selfSprite.sprite = sprites[1];
    }

    void three()
    {
        selfSprite.sprite = sprites[2];
        
    }

    void four()
    {
        Hurt();
        selfSprite.sprite = sprites[3];
    }
}
