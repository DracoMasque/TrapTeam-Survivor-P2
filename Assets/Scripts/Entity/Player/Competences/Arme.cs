using UnityEngine;

public abstract class Arme : MonoBehaviour
{
    public string name;
    [TextArea(5,20)] public string description;
    public Sprite image;
    public float damage = 10;
    public Rarity rarity;
    public TypeArme type;
    
    public int armeID;

    public abstract void Amelioration();
    
    void Attack(EnemyPrefab target)
    {
        target.TakeDamage(damage);
        foreach(Equipement e in PlayerEquiment.instance.equipements)
        {
            if (e.GetComponent<Vampirism>())
            {
                e.GetComponent<Vampirism>().VampHeal(damage);
            }
        }
    }
}
