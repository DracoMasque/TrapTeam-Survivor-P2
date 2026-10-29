using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public float spawnChance;
    public ElementWeakScriptable elements;
    public float bonusElement;
    [SerializeField] private bool alreadyStrong = false;
    [SerializeField] private bool alreadyWeak = false;

    private void ChangeBonusElement(Elements element)
    {
        if (element == elements.weakness && !alreadyWeak)
        {
            print(gameObject.name + " " +"weakness before : "  + bonusElement/2);
            alreadyWeak = true;
            bonusElement /= 2;
            print(gameObject.name + " " +"weakness after : "  + bonusElement);
        }
        else if (element == elements.strength && !alreadyStrong)
        {
            alreadyStrong = true;
            bonusElement *= 2;
        }
    }

    private void RetireChangeBonusElement(Elements element)
    {
        if (element == elements.weakness && alreadyWeak)
        {
            alreadyWeak = false;
            bonusElement *= 2;
        }
        else if (element == elements.strength && alreadyStrong)
        {
            alreadyStrong = false;
            bonusElement /= 2;
        }
    }
}
