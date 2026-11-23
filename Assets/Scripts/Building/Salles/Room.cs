using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public string name;
    public float spawnChance;
    public ElementWeakScriptable elements;
    public float bonusElement;
    [SerializeField] private bool alreadyStrong = false;
    [SerializeField] private bool alreadyWeak = false;
    [SerializeField] private GameObject effectStrong;

    private void ChangeBonusElement(Elements element)
    {
        if (element == elements.weakness && !alreadyWeak)
        {
            alreadyWeak = true;
            bonusElement /= 2;
        }
        else if (element == elements.strength && !alreadyStrong)
        {
            alreadyStrong = true;
            bonusElement *= 2;
            effectStrong.SetActive(true);
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
            effectStrong.SetActive(false);
        }
    }
}
