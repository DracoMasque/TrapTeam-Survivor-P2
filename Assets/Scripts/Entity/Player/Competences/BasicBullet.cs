using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class BasicBullet : MonoBehaviour
{
    protected GameObject target;
    protected float damage = 10;
    protected float speed;
    private int pourcentageAmelioration = 10;
    
    
    protected Vector3 direction = Vector3.right;

    public virtual void Init(GameObject _target, float _damage, float _speed)
    {
        target = _target;
        damage = _damage;
        speed = _speed;
        
        direction = target.transform.position - transform.position;
        direction.Normalize();
        Destroy(gameObject,10f);
    }
    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyPrefab>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
