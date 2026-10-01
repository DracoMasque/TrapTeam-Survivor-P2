using System;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    [SerializeField] private float speedPourcentage;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameManager.paused) return;
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Controlleur>().speed -= col.gameObject.GetComponent<Controlleur>().speed*(speedPourcentage / 100);
        }
        else if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyPrefab>().speed -= col.gameObject.GetComponent<EnemyPrefab>().speed*(speedPourcentage / 100);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameManager.paused) return;
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Controlleur>().speed = other.gameObject.GetComponent<Controlleur>().maxSpeed;
        }
        else if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPrefab>().speed = other.gameObject.GetComponent<EnemyPrefab>().maxSpeed;
        }
    }
}
