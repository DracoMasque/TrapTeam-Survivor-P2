using UnityEngine;

public class UpgradeStrength : Upgrade
{
    public override void Selected(Controlleur player)
    {
        if (!player.alreadyBoosted)
        {
            player.damage += Mathf.Ceil((player.damage - player.oldModDamage) * (modifier / 100f));
        }
        else
        {
            player.damage += Mathf.Ceil((player.damageBaseWhenBoosted - player.oldModDamage) * (modifier / 100f));
            player.damageBaseWhenBoosted += Mathf.Ceil((player.damageBaseWhenBoosted - player.oldModDamage) * (modifier / 100f));
        }
        
        player.RedoDamageMod();
    }
}
