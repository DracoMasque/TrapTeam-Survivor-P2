using UnityEngine;
using UnityEngine.UI;

public abstract class Equipement : MonoBehaviour
{
    public string name;
    [TextArea(5,20)] public string description;
    public Image image;
    public Rarity rarity;

    public void OnPickUp()
    {
        
    }

    public void EquipementAction()
    {
        
    }
}
