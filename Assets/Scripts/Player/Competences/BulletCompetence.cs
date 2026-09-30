using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletCompetence : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    public int damage = 10;
    public float speed = 10;
    private Cooldown timer;
    private DetectEnemy detectEnemy;
    private GameObject player;
    
    List<GameObject> enemies;
    [HideInInspector]
    public GameObject target;
    
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GetComponent<Cooldown>();
        detectEnemy = GetComponent<DetectEnemy>();
        enemies = detectEnemy.enemies;
    }

    void Update()
    {
        if (timer.finished && detectEnemy.enemies.Count > 0)
        {
            LaunchBullet();
            timer.Play();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void LaunchBullet()
    {
        target = TargetEnemy();
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<BasicBullet>().Init(target, damage, speed);
    }

    GameObject TargetEnemy()
    {
        //Get the player
        //Get every body that have the tag monster in collision shape Collider2D.GetShapes(PhysicsSahpeType2D.Circle)
        //Calculate all of their distance to the player
        //Sort them and take the smallest one
        SortedDictionary<float,GameObject> distanceList = new SortedDictionary<float, GameObject>();
        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, player.transform.position);
            distanceList.Add(distance, enemy.gameObject);
        }
        GameObject result = distanceList.ElementAt(0).Value;
        return result;
    }
}
