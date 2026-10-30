using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public string name;
    [TextArea(5,20)] public string description;
    public Sprite image;
    public Rarity rarity;
    
    [Range(10f, 30.0f)] public float modifier;

    public abstract void Selected(Controlleur player);
}
