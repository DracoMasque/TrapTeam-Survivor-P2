using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectRarity", menuName = "Scriptable Objects/ScriptableObjectRarity")]
public class ScriptableObjectRarity : ScriptableObject
{
    public Rarity rarity;
    [Tooltip("in %")]
    public float dropRate = 1;
    public List<GameObject> drops;

    public GameObject RandomDrop()
    {
        GameObject drop = null;
        
        int chance = Random.Range(0, 100);
        if (chance < dropRate)
        {
            int objectChance = Random.Range(0, drops.Count);
            drop = drops[objectChance];
            //Debug.Log(rarity);
        }
        
        return drop;
    }
}
