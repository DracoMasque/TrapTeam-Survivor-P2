using UnityEngine;

public class Fontaine : MonoBehaviour
{
    [SerializeField] private GameObject healPrefab;
    private Cooldown cooldown;
        
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = SpawnHeart;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Heal>() != null)
        {
            cooldown.Play();
        }
    }
    void SpawnHeart()
    {
        Instantiate(healPrefab, transform.position, transform.rotation);
    }
}
