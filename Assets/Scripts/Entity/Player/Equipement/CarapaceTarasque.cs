using UnityEngine;

public class CarapaceTarasque : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    private float appliedMod;
    
    public override void OnPickUp(Controlleur player)
    {
        player.RedoSpeedMod();
    }

    public void OnMaxDefenseChange(Controlleur player)
    {
        appliedMod = Mathf.Ceil((player.defense - player.oldmodDefense) * (modifier / 100));
        
        player.modDefense += appliedMod;
    }

    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
        player.RedoSpeedMod();
    }

    public override void EquipementAction()
    {
        return;
    }
}
