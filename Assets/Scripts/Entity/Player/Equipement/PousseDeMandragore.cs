using UnityEngine;
using UnityEngine.UI;

//Si c'est le seul il fait en sorte que les enemis peuvent aussi drop du heal
//Sinon il augment le rate des drop en modifiant le Scriptable object HealEnemyDrop
public class PousseDeMandragore : Equipement
{
    [SerializeField][Range(1,100)] int chance = 10;
    [SerializeField] private ScriptableObjectRarity healEnemyDrop;
    public override void OnPickUp(Controlleur player)
    {
        OnEnemyHealDropRateChanged();
    }
    public override void LetGoEquipement(Controlleur player)
    {
        OnEnemyHealDropRateChanged();
        Destroy(gameObject);
    }
    
    public void OnEnemyHealDropRateChanged()
    {
        float finalChance = 0;
        foreach (Equipement e in PlayerEquiment.instance.equipements)
        {
            if (e.GetComponent<PousseDeMandragore>())
            {
                finalChance += chance;
            }
        }
        healEnemyDrop.dropRate = finalChance;
    }
    
    public override void EquipementAction()
    {
        return;
    }

}