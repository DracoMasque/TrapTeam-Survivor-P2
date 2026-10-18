using UnityEngine;
using UnityEngine.EventSystems;

public class BasicBullet : MonoBehaviour
{
    GameObject target;
    int damage = 10;
    float speed;
    
    Vector3 direction = Vector3.right;
    /*void Start()
    {
        target = GetComponentInParent<BulletCompetence>().target;
        damage = GetComponentInParent<BulletCompetence>().damage;
        speed = GetComponentInParent<BulletCompetence>().speed;
    }*/

    public void Init(GameObject _target, int _damage, float _speed)
    {
        target = _target;
        damage = _damage;
        speed = _speed;
        
        direction = target.transform.position - transform.position;
        direction.Normalize();
    }
    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyPrefab>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
