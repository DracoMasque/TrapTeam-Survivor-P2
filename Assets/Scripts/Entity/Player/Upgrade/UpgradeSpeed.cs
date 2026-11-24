using UnityEngine;

public class UpgradeSpeed : Upgrade
{
    public override void Selected(Controlleur player)
    {
        if (!player.alreadyBoosted)
        {
            float toAdd = Mathf.Ceil((player.speed - player.oldModSpeedTrap) * (modifier / 100f));
            player.speed += toAdd;
        }
        else
        {
            float toAdd = Mathf.Ceil((player.speedBaseWhenBoosted - player.oldModSpeedTrap) * (modifier / 100f));
            player.speed += toAdd;
            player.speedBaseWhenBoosted += toAdd;
        }
        
        player.RedoSpeedMod();
    }
}
