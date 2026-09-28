using Unity.VisualScripting;
using UnityEngine;

public class XP : MonoBehaviour
{
    private Transform posJoueur;
    [SerializeField] private float speedXp = 10f;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, posJoueur.position, speedXp * Time.deltaTime);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            posJoueur = other.transform;
           
        }
    }
}
