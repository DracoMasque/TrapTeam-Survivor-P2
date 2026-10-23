using Unity.VisualScripting;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float healthGiven = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Controlleur>().Heal(healthGiven);
            Destroy(gameObject);
        }
    }
}
