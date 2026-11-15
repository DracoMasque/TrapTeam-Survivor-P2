using UnityEngine;

public class CoeurDragon : Equipement
{
    public Cooldown cooldown;
    [Tooltip("in %")] [Range(1f, 10.0f)] public float healAmount;
    
    public override void OnPickUp(Controlleur player)
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = EquipementAction;
    }
    
    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
    }

    public override void EquipementAction()
    {
        Controlleur.instance.Heal(Controlleur.instance.health * (healAmount / 100f));
    }
}
