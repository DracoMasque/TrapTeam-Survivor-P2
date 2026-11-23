using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaguetteMagique : Arme
{
    public float speed = 10;
    private Cooldown timer;
    private DetectEnemy detectEnemy;
    private GameObject player;
    
    List<GameObject> enemies;
    [HideInInspector]
    public GameObject target;
    public int pourcentageDamage = 10;
    private GameObject laser;
    
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GetComponent<Cooldown>();
        timer.OnTimeout = OnTimeout;
        detectEnemy = GetComponent<DetectEnemy>();
        enemies = detectEnemy.enemies;
    }

    void OnTimeout()
    {   
        if (timer.finished && detectEnemy.enemies.Count > 0)
        {
            LaserBeam();
            timer.Play();
        }
        
    }

    private void LaserBeam()
    {
        Vector3 playerPos = player.transform.position;
        GameObject enemy = TargetEnemy();
        Vector3 enemyPos = enemy.transform.position;
        GameObject instantiate = Instantiate(laser, playerPos,transform.rotation);
        instantiate.transform.LookAt(enemyPos);

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
    
    public override void Amelioration()
    {
        
    }
}
