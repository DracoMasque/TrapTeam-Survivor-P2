using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    [SerializeField] private float speedPourcentage;

    void OnTriggerEnter2D(Collider2D col)
    {
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
