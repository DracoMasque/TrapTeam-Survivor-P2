using UnityEngine;

public class Brulure : Etat
{
    public Cooldown cooldown;
    public float degats = 5;
    private EnemyPrefab enemy;

    void Start()
    {
        cooldown.OnTimeout = Burn;
        enemy = GetComponent<EnemyPrefab>();
    }

    void Burn()
    {
        enemy.currentHealth -= degats * (1 - (enemy.defense / 100));
    }

    public override void EndEtat()
    {
        enemy.etat = null;
        Destroy(gameObject);
    }
}
