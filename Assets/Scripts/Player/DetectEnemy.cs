using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    //[HideInInspector]
    public List<GameObject> enemies;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
        }
    }
}
