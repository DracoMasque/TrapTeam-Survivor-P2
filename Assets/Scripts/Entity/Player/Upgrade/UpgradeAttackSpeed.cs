using UnityEngine;

public class UpgradeAttackSpeed : Upgrade
{
    public override void Selected(Controlleur player)
    {
        player.damageFreq += Mathf.Ceil((player.damageFreq - player.OldModDamageFreq) * (modifier / 100));
        player.RedoDamageFreqMod();
    }
}
