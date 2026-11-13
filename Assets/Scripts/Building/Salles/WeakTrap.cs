using UnityEngine;
using UnityEngine.Serialization;

public class WeakTrap : MonoBehaviour
{
    [SerializeField] private float weakPourcentage;
    private GameManager gameManager;
    private float damagePlayer;
    private float damageEnemy;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            damagePlayer = col.gameObject.GetComponent<Controlleur>().damage;
            col.gameObject.GetComponent<Controlleur>().RedoDamageModTrap(-col.gameObject.GetComponent<Controlleur>().modDamage * (weakPourcentage / 100));
        }
        else if (col.tag == "Enemy")
        {
            damageEnemy = col.gameObject.GetComponent<EnemyPrefab>().damage;
            col.gameObject.GetComponent<EnemyPrefab>().RedoSpeedModTrap(-col.gameObject.GetComponent<EnemyPrefab>().modDamage * (weakPourcentage / 100));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Controlleur>()
                .DamageReturn(damagePlayer * (weakPourcentage / 100));
            other.gameObject.GetComponent<Controlleur>().RedoDamageMod();
        }
        else if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPrefab>().DamageReturn(damageEnemy * (weakPourcentage / 100));
        }
    }
}
