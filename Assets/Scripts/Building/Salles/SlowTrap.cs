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
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Controlleur>().modSpeedTrap -= col.gameObject.GetComponent<Controlleur>().modSpeedTrap * (speedPourcentage / 100);
        }
        else if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyPrefab>().modSpeedTrap -= col.gameObject.GetComponent<EnemyPrefab>().modSpeedTrap * (speedPourcentage / 100);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Controlleur>().modSpeedTrap = (100 / speedPourcentage) * other.gameObject.GetComponent<Controlleur>().modSpeedTrap;
        }
        else if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPrefab>().modSpeedTrap = (100 / speedPourcentage) * other.gameObject.GetComponent<EnemyPrefab>().modSpeedTrap;
        }
    }
}
