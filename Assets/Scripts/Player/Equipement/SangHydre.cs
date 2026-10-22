using UnityEngine;

public class SangHydre : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    
    public override void OnPickUp(Controlleur player)
    {
        player.maxHealth = Mathf.Ceil(player.maxHealth * 1 + (modifier / 100));
        player.currentHealth = Mathf.Ceil(player.currentHealth * 1 + (modifier / 100));
    }

    public void OnMaxHealthChange(Controlleur player)
    {
        player.maxHealth = Mathf.Ceil(player.maxHealth * 1 + (modifier / 100));
        player.currentHealth = Mathf.Ceil(player.currentHealth * 1 + (modifier / 100));
    }

    public override void EquipementAction()
    {
        return;
    }
}
