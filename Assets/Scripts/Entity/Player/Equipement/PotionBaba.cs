using UnityEngine;

public class PotionBaba : Equipement
{
    [SerializeField] [Range(10f, 15.0f)] private float modifier;
    private float appliedMod;
    
    public override void OnPickUp(Controlleur player)
    {
        player.RedoCritChanceMod();
    }
    
    public void OnMaxCritChanceChange(Controlleur player)
    {
        appliedMod = Mathf.Ceil((player.critChance - player.oldModCritChance) * (modifier / 100));
        
        player.modCritChance += appliedMod;
    }
    
    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
        player.RedoCritChanceMod();
    }

    public override void EquipementAction()
    {
        return;
    }
}
