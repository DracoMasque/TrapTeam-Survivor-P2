using UnityEngine;

public class EssenceVelociraptor : Equipement
{
    [SerializeField] [Range(10f, 15.0f)] private float modifier;
    private float appliedMod;
    
    public override void OnPickUp(Controlleur player)
    {
        player.RedoDamageFreqMod();
    }
    
    public void OnMaxDamageFreqChange(Controlleur player)
    {
        appliedMod = Mathf.Ceil((player.damageFreq - player.OldModDamageFreq) * (modifier / 100));
        
        player.modDamageFreq += appliedMod;
    }
    
    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
        player.RedoDamageFreqMod();
    }

    public override void EquipementAction()
    {
        return;
    }
}
