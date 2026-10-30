using UnityEngine;

public class UpgradeStrength : Upgrade
{
    public override void Selected(Controlleur player)
    {
        player.damage += Mathf.Ceil((player.speed - player.oldModDamage) * (modifier / 100f));
        player.RedoDamageMod();
    }
}
