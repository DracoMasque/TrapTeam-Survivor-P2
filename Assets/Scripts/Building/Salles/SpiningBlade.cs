using UnityEngine;

public class SpiningBlade : MonoBehaviour
{
    public int degatSpiningBlade;
   
    
    
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Controlleur>().currentHealth -= degatSpiningBlade;
            
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Room parentRoom = GetComponentInParent<Room>();
            collision.gameObject.GetComponent<EnemyPrefab>().TakeDamageTrap(degatSpiningBlade, parentRoom.elements.elements, parentRoom.bonusElement);
        }
    }
}
