using UnityEngine;

public class Gel : Etat
{
    private EnemyPrefab enemy;
    private float speedPourcentage;
    private float speedEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedEnemy = enemy.speed;
        enemy.RedoSpeedModTrap(-enemy.speed * (speedPourcentage / 100));
    }

    public override void EndEtat()
    {
        enemy.SpeedReturn(-speedEnemy * (speedPourcentage / 100));
        enemy.etat = null;
        Destroy(gameObject);
    }
}
