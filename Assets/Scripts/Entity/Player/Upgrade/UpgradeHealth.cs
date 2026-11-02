using UnityEngine;

public class UpgradeHealth : Upgrade
{
    public override void Selected(Controlleur player)
    {
        player.health += Mathf.Ceil((player.health - player.oldModHealth) * (modifier / 100f));
        player.RedoHealthMod();
    }
}
