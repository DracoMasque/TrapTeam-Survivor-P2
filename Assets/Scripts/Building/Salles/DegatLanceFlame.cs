using System;
using UnityEngine;

public class DegatLanceFlame : MonoBehaviour
{
    public int degatLanceFlame;
    public DeplacementLanceFlame deplacementLanceFlame;

    private void Start()
    {
        deplacementLanceFlame = GetComponent<DeplacementLanceFlame>();
    }
    
    void Update()
    {
        deplacementLanceFlame.Aller();
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Controlleur>().currentHealth -= degatLanceFlame;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Room parentRoom = GetComponentInParent<Room>();
            collision.gameObject.GetComponent<EnemyPrefab>()
                .TakeDamageTrap(degatLanceFlame, parentRoom.elements.elements, parentRoom.bonusElement);
        }
    }
}
