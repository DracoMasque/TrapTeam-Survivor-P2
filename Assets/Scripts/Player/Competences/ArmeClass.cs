using UnityEngine;

public class Arme : MonoBehaviour
{
    public string name;
    [TextArea(5,20)] public string description;
    public Sprite image;
    public int damage = 10;
    //public Rarity rarity;
}
