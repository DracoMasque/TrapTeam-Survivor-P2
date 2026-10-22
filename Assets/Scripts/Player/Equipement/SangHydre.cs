using UnityEngine;

public class SangHydre : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    
    public override void OnPickUp(Controlleur player)
    {
        Mathf.Ceil(player.maxHealth *= 1 + (modifier / 100));
        Mathf.Ceil(player.currentHealth *= 1 + (modifier / 100));
    }

    public void OnMaxHealthChange(Controlleur player)
    {
        Mathf.Ceil(player.maxHealth *= 1 + (modifier / 100));
        Mathf.Ceil(player.currentHealth *= 1 + (modifier / 100));
    }

    public override void EquipementAction()
    {
        return;
    }
}
