using UnityEngine;

public class SangHydre : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    
    //J'ai pas compris ce qui était fait/la différence entre OnMaxHealthChange et OnPickUp
    //Ou pourquoi on utilise l'un et pas l'autre
    
    public override void OnPickUp(Controlleur player)
    {
        player.modHealth = Mathf.Ceil(player.modHealth * 1 + (modifier / 100));
        player.currentHealth = Mathf.Ceil(player.currentHealth * 1 + (modifier / 100));
    }

    public void OnMaxHealthChange(Controlleur player)
    {
        player.modHealth = Mathf.Ceil(player.modHealth * 1 + (modifier / 100));
        player.currentHealth = Mathf.Ceil(player.currentHealth * 1 + (modifier / 100));
    }

    public override void EquipementAction()
    {
        return;
    }
}
