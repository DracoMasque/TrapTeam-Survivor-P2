using UnityEngine;

public class XpRecup : MonoBehaviour
{
    
    public int valueXp;
    void OnTriggerEnter2D(Collider2D xp)
    {
        if (xp.tag.Equals("Player"))
        {
            Controlleur player = xp.gameObject.GetComponent<Controlleur>();
            player.currentXp += valueXp;
            if (player.currentXp >= player.maxXp)
            {
                player.currentXp = player.maxXp;
            }

            Destroy(gameObject);
        }
    }
}
