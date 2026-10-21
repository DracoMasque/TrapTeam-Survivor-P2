using UnityEngine;

public class SangHydre : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    
    public void OnPickUp(Controlleur player)
    {
        player.maxHealth += player.maxHealth * (1 + (modifier/100));
        player.currentHealth += player.currentHealth * (1 + (modifier/100));
    }

    public void OnMaxHealthChange(Controlleur player)
    {
        player.maxHealth += player.maxHealth * (1 + (modifier/100));
        player.currentHealth += player.currentHealth * (1 + (modifier/100));
    }
}
