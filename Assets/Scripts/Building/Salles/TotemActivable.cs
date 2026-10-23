using System.Collections.Generic;
using UnityEngine;

public class TotemActivable : MonoBehaviour
{
    private Cooldown cooldown;
    public int degatTotem;
    private List<GameObject> entite = new List<GameObject>();
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = DegatTotem;
    }
    

    void OnTriggerStay2D (Collider2D collision)
    {
        if (entite.Count < 1 && cooldown.finished)
        {
            cooldown.Play();
        }
        entite.Add(collision.gameObject);
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        entite.Remove(collision.gameObject);
    }

    void DegatTotem()
    {
        foreach (GameObject p in entite)
        {
             if (p.tag == "Player")
                    {
                        p.GetComponent<Controlleur>().currentHealth -= degatTotem;
                        
                    }
            
                    if (p.tag == "Enemy")
                    {
                        p.GetComponent<EnemyPrefab>().TakeDamage(degatTotem);
                    }
        }
       
    }
}
