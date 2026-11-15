using UnityEngine;
using UnityEngine.UI;

//Chance de redonner de la vie au joueur quand il tappe un enemis
public class Vampirism : Equipement
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
        Destroy(gameObject);
    }
    
    //VampHeal se trouve dans la fonction Attack des Armes. Chaques Arme doit appeler au moin la fonction Attack pour faire des degats
    public void VampHeal(float damage)
    {
        Controlleur player = Controlleur.instance;
        int randChance = Random.Range(0, 100);
        if (randChance <= chance)
        {
            player.Heal(damage * (healRatio / 100f));
        }
    }
}