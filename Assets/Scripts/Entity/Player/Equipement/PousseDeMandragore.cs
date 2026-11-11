using UnityEngine;
using UnityEngine.UI;

public class PousseDeMandragore : Equipement
{
    [SerializeField][Range(1,100)] int chance = 10;
    [SerializeField][Range(10,100)] int healRatio = 10;
    
    public override void OnPickUp(Controlleur player)
    {
        return;
    }

    public override void EquipementAction()
    {
        return;
    }

    public override void LetGoEquipement(Controlleur player)
    {
        return;
    }
    
}