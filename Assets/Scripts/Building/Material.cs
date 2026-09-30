using UnityEngine;

public class Material : MonoBehaviour
{
    public int value;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerBuilding player = collision.gameObject.GetComponent<PlayerBuilding>();
            player.numberMaterial += value;
            if (player.numberMaterial >= player.requarieredMaterial)
            {
                player.numberMaterial = player.requarieredMaterial;
            }
            Destroy(gameObject);
        }
    }
}
