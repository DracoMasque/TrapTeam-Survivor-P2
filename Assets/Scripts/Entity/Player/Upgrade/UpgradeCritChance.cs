using UnityEngine;

public class UpgradeCritChance : Upgrade
{
    public override void Selected(Controlleur player)
    {
        player.damageFreq += Mathf.Ceil((player.critChance - player.oldModCritChance) * (modifier / 100));
        player.RedoCritChanceMod();
    }
}
