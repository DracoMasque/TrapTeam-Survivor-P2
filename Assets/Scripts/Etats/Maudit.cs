using UnityEngine;

public class Maudit : Etat
{
    private EnemyPrefab enemy;
    [Range(1f, 100f)] public float propagationChance;
    public float propagationRange;
    
    public override void StartEtat()
    {
        enemy = GetComponentInParent<EnemyPrefab>();
    }
    
    public override void EndEtat()
    {
        enemy.etat = null;
        Destroy(gameObject);
    }
}
