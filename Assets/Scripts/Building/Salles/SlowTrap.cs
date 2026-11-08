using System;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    [SerializeField] private float speedPourcentage;
    private GameManager gameManager;
    private float speedPlayer;
    private float speedEnemy;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            speedPlayer = col.gameObject.GetComponent<Controlleur>().speed;
            col.gameObject.GetComponent<Controlleur>().RedoSpeedModTrap(-col.gameObject.GetComponent<Controlleur>().speed * (speedPourcentage / 100));
        }
        else if (col.tag == "Enemy")
        {
            speedEnemy = col.gameObject.GetComponent<EnemyPrefab>().speed;
            col.gameObject.GetComponent<EnemyPrefab>().RedoSpeedModTrap(-col.gameObject.GetComponent<EnemyPrefab>().speed * (speedPourcentage / 100));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Controlleur>()
                .SpeedReturn(speedPlayer * (speedPourcentage / 100));
            other.gameObject.GetComponent<Controlleur>().RedoSpeedMod();
        }
        else if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPrefab>().SpeedReturn(speedEnemy * (speedPourcentage / 100));
        }
    }
}
