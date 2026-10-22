using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public List<ScriptableObjectRarity> rarities;
    public List<List<ScriptableObjectRarity>> possibilities; //possibilités[raretés[so],[so]], raretés2[so,...
    
    private void Start()
    {
        SortRarities();
        GetRandomDrop();
    }

    public GameObject GetRandomDrop()
    {
        GameObject newDrop = null;
        foreach (List<ScriptableObjectRarity> rarity in possibilities)
        {
            foreach (ScriptableObjectRarity rarityItem in rarity)
            {
                newDrop = rarityItem.RandomDrop();
                if (newDrop != null)
                {
                    return newDrop;
                }
            }
        }
        return newDrop;
    }

    //get every rarity, sort them from highest rarity to lowest
    //first rarity will always be 100%
    private void SortRarities()
    {
        rarities.Sort((a, b) => a.rarity.CompareTo(b.rarity));
        rarities[0].dropRate = 100f;
       
        
        //go through every rarity, add the common to the same i and all the uncommon to the same i
        
        rarities.Reverse();
    }
}
