using UnityEngine;

public class Gel : Etat
{
    private EnemyPrefab enemy;
    public float speedPourcentage;
    private float speedEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void StartEtat()
    {
        enemy = GetComponentInParent<EnemyPrefab>();
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
