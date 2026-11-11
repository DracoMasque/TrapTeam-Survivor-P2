using UnityEngine;
using UnityEngine.Serialization;

public class WeakTrap : MonoBehaviour
{
    [SerializeField] private float weakPourcentage;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Controlleur>().RedoSpeedModTrap(-col.gameObject.GetComponent<Controlleur>().modDamage * (weakPourcentage / 100));
        }
        else if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyPrefab>().RedoSpeedModTrap(-col.gameObject.GetComponent<EnemyPrefab>().modDamage * (weakPourcentage / 100));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Controlleur>().RedoDamageMod();
        }
        else if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPrefab>().SpeedReturn(other.gameObject.GetComponent<EnemyPrefab>().modDamage * (weakPourcentage / 100));
        }
    }
}
