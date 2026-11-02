using UnityEngine;

public class GriffesGarous : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    private float appliedMod;
    
    public override void OnPickUp(Controlleur player)
    {
        player.RedoDamageMod();
    }

    public void OnMaxDamageChange(Controlleur player)
    {
        appliedMod = Mathf.Ceil((player.damage - player.oldModDamage) * (modifier / 100));
        
        player.modDamage += appliedMod;
    }

    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
        player.RedoDamageMod();
    }

    public override void EquipementAction()
    {
        return;
    }
}
