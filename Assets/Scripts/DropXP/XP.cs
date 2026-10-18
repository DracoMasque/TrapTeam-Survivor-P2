using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class XP : MonoBehaviour
{
    private Transform pos;
    [SerializeField] private float speedXp = 10f;
    
    
    void Update()
    {
        if (pos)
        {
            pos.position = Vector2.MoveTowards( pos.position,transform.position, speedXp * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("XP") || other.tag.Equals("Material"))
        {
            pos = other.transform;
        }
        
    }
}
