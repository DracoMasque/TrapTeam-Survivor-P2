using System.Collections.Generic;
using UnityEngine;

public class Confusion : Etat
{
    private EnemyPrefab enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void StartEtat()
    {
        enemy = GetComponentInParent<EnemyPrefab>();
        GetClosest();
        print("etat");
    }

    void GetClosest()
    {
        List<GameObject> closeEnemy = enemy.gameObject.GetComponentInParent<DetectEnemy>().enemies;
        if (closeEnemy.Count > 1)
        {
            enemy.closestEnemy = closeEnemy[0];
            for (int i = 1; i < closeEnemy.Count; i++)
            {
                if (Vector2.Distance(closeEnemy[i].transform.position, enemy.transform.position) >
                    Vector2.Distance(enemy.closestEnemy.transform.position, enemy.transform.position))
                {
                    enemy.closestEnemy = closeEnemy[i];
                }
            }
        }
    }

    public override void EndEtat()
    {
        enemy.etat = null;
        Destroy(gameObject);
    }
}
