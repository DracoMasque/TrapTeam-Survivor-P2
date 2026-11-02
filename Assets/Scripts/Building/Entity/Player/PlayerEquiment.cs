using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquiment : MonoBehaviour
{
    public static PlayerEquiment instance;
    
    public List<Equipement> equipements;
    public int maxEquipements = 5;

    private void Update()
    {
        instance = this;
        foreach (Equipement equipement in equipements)
        {
            equipement.EquipementAction();
        }
    }
}
