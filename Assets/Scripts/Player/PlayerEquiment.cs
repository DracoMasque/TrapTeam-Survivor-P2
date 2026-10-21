using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquiment : MonoBehaviour
{
    public List<Equipement> equipements;
    public int maxEquipements = 5;

    private void Update()
    {
        foreach (Equipement equipement in equipements)
        {
            equipement.EquipementAction();
        }
    }
}
