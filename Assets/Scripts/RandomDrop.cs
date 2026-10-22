using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public List<ScriptableObjectRarity> rarities;

    private void Start()
    {
        SortRarities();
        GetRandomDrop();
    }

    public GameObject GetRandomDrop()
    {
        GameObject newDrop = null;
        foreach (ScriptableObjectRarity rarity in rarities)
        {
            newDrop = rarity.RandomDrop();
            if (newDrop != null)
            {
                break;
            }
        }
        return newDrop;
    }
    public void GetRandomDrop(int a)
    {
        
    }
    //get every rarity, sort them from highest rarity to lowest
    //first rarity will always be 100%
    private void SortRarities()
    {
        rarities.Sort((a, b) => a.rarity.CompareTo(b.rarity));
        rarities[0].dropRate = 100f;
        rarities.Reverse();
    }
}
