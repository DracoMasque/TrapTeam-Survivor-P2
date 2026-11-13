using UnityEngine;

public abstract class Arme : MonoBehaviour
{
    public string name;
    [TextArea(5,20)] public string description;
    public Sprite image;
    public float damage = 10f;
    public Rarity rarity;
    public TypeArme type;

    public int armeID;

    public abstract void Amelioration();
}
