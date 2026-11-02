using UnityEngine;

public class UpgradeSpeed : Upgrade
{
    public override void Selected(Controlleur player)
    {
        player.speed += Mathf.Ceil((player.speed - player.oldModSpeedTrap) * (modifier / 100f));
        player.RedoSpeedMod();
    }
}
