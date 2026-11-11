using UnityEngine;

//Augment le rate des drop de materiaux en modifiant le Scriptable object RareEnemyDrop
public class EncyclopedieDesNains : Equipement
{
    [SerializeField][Range(1,100)] int chance = 50;
    [SerializeField] private ScriptableObjectRarity rareEnemyDrop;
    private static float _baseDropRate;
    
    public override void OnPickUp(Controlleur player)
    {
        if (_baseDropRate == 0) _baseDropRate = rareEnemyDrop.dropRate;
        OnMaterialDropRateChanged();
    }
    public override void LetGoEquipement(Controlleur player)
    {
        OnMaterialDropRateChanged();
        Destroy(gameObject);
    }
    
    public void OnMaterialDropRateChanged()
    {
        float finalChance = _baseDropRate;
        foreach (Equipement e in PlayerEquiment.instance.equipements)
        {
            if (e.GetComponent<PousseDeMandragore>())
            {
                finalChance *= (chance / 100f);
            }
        }
        rareEnemyDrop.dropRate = finalChance;
    }
    
    public override void EquipementAction()
    {
        return;
    }
}
