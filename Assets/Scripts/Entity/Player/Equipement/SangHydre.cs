using UnityEngine;

public class SangHydre : Equipement
{
    [SerializeField] [Range(5f, 10.0f)] private float modifier;
    private float appliedMod;
    
    //J'ai pas compris ce qui était fait/la différence entre OnMaxHealthChange et OnPickUp
    //Ou pourquoi on utilise l'un et pas l'autre
    
    public override void OnPickUp(Controlleur player)
    {
        player.RedoHealthMod();
    }

    public void OnMaxHealthChange(Controlleur player)
    {
        
        appliedMod = Mathf.Ceil((player.health - player.oldModHealth) * (modifier / 100));
        print("appliedMod : " + appliedMod);
        
        player.modHealth += appliedMod;
    }

    public override void LetGoEquipement(Controlleur player)
    {
        Destroy(gameObject);
        player.RedoHealthMod();
    }

    public override void EquipementAction()
    {
        return;
    }
}
