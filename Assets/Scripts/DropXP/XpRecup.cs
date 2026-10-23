using UnityEngine;

public class XpRecup : MonoBehaviour
{
    
    public int valueXp;
    void OnTriggerEnter2D(Collider2D xp)
    {
        if (xp.tag.Equals("Player"))
        {
            Controlleur player = xp.gameObject.GetComponent<Controlleur>();
            LevelUPManager.instance.AddXp(valueXp);
            Destroy(gameObject);
        }
    }
}
