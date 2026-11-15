using System;
using UnityEngine;

public class DegatPique : MonoBehaviour
{
    public  int degatPique;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Controlleur>().currentHealth -= degatPique;
            
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Room parentRoom = GetComponentInParent<Room>();
            collision.gameObject.GetComponent<EnemyPrefab>().TakeDamageTrap(degatPique, parentRoom.elements.elements, parentRoom.bonusElement);
        }
    }
}
