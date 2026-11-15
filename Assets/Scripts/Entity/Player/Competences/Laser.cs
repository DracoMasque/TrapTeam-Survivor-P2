using UnityEngine;

public class Laser : MonoBehaviour
{
    private float damage = 1;
    void Start()
    {
        Destroy(this,0.1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           collision.gameObject.GetComponent<EnemyPrefab>().TakeDamage(damage);
        }
    }
}
