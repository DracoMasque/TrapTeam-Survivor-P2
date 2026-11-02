using UnityEngine;
using UnityEngine.UI;

public abstract class Equipement : MonoBehaviour
{
    public string name;
    [TextArea(5,20)] public string description;
    public Sprite image;
    public Rarity rarity;

    public abstract void OnPickUp(Controlleur player);


    public abstract void EquipementAction();

    public abstract void LetGoEquipement(Controlleur player);

}
