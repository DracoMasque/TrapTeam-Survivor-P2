using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class XP : MonoBehaviour
{
    public int valueXp;
    
    void OnTriggerEnter2D(Collider2D xp)
    {
        if (xp.CompareTag("Player"))
        {
            Controlleur player = xp.gameObject.GetComponent<Controlleur>();
            LevelUPManager.instance.AddXp(valueXp);
            Destroy(gameObject);
        }
    }
    
    //Jsp a quoi ça sert ça
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag.Equals("XP") || other.tag.Equals("Material"))
    //    {
    //        pos = other.transform;
    //    }
    //}
}
