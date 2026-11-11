using UnityEngine;

public class UpgradeDefense : Upgrade
{
    public override void Selected(Controlleur player)
    {
        player.defense += Mathf.Ceil((player.defense - player.oldmodDefense) * (modifier / 100f));
        player.RedoDefenseMod();
    }
}
