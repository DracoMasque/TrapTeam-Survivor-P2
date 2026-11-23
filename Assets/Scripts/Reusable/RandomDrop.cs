using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomDrop : MonoBehaviour
{
    public List<ScriptableObjectRarity> objects;
    [HideInInspector] public List<List<ScriptableObjectRarity>> possibilities = new List<List<ScriptableObjectRarity>>();

    private void Start()
    {
        SortRarities();
    }

    //return un gameObject random parmis la liste
    public GameObject GetRandomDrop()
    {
        GameObject newDrop = null;
        foreach (List<ScriptableObjectRarity> rarity in possibilities)
        {
            var index = Random.Range(0, rarity.Count);
            newDrop = rarity[index].RandomDrop();
            if (newDrop != null)
            {
                return newDrop;
            }
        }

        return newDrop;
    }

    //get every rarity, sort them from highest rarity to lowest
    //go through every rarity, add the Common Rarity to the same index same for Uncommon etc...
    //lowest rarity will always be 100%
    
    //possibilities -> [Common[ScriptableObject],[ScriptableObject]], Uncommon[ScriptableObject,...
    private void SortRarities()
    {
        List<Rarity> existing_rarity =  new List<Rarity>();
        objects.Sort((a, b) => a.rarity.CompareTo(b.rarity));
        existing_rarity.Sort((a, b) => a.CompareTo(b));
        
        foreach (ScriptableObjectRarity _object in objects)
        {
            if (!existing_rarity.Contains(_object.rarity))
            {
                existing_rarity.Add(_object.rarity);
            }
        }
        
        foreach (ScriptableObjectRarity _object in objects)
        {
            foreach (Rarity rarity in existing_rarity)
            {
                if (rarity == _object.rarity)
                {
                    int index = existing_rarity.IndexOf(rarity);

                    if (index == -1)
                    {
                        Debug.LogError("RandomDrop.cs l.58 : rarity not found");
                        break;
                    }

                    if (index == 0)
                    {
                        _object.dropRate = 100f;
                    }
                    
                    if (possibilities.Count <= index)
                    {
                        var temp = new List<ScriptableObjectRarity>();
                        temp.Add(_object);
                        possibilities.Add(temp);
                    }
                    else
                    {
                        possibilities[index].Add(_object);
                    }
                    
                }
            }
        }

        possibilities.Reverse();
    }
    
}
