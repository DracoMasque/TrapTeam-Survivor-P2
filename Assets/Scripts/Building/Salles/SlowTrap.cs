using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    [SerializeField] private float speedPourcentage;
    private GameManager gameManager;

    void start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameManager.paused) return;
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Controlleur>().speed *= speedPourcentage/100;
        }
        else if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyPrefab>().speed *= speedPourcentage/100;
        }
    }
}
