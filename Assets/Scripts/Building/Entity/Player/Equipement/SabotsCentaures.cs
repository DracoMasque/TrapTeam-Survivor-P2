using UnityEngine;

public class SabotsCentaures : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    private float appliedMod;
    
    public override void OnPickUp(Controlleur player)
    {
        player.RedoSpeedMod();
    }

    public void OnMaxSpeedChange(Controlleur player)
    {
        appliedMod = Mathf.Ceil((player.speed - player.oldModSpeedTrap) * (modifier / 100));
        
        player.modSpeedTrap += appliedMod;
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