using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Groupement : MonoBehaviour
{
    [SerializeField] private List<GameObject> groupedRooms;
    [SerializeField] private Room[] sallePossible;

    private void Start()
    {
        sallePossible = Resources.LoadAll<Room>("BuildingPrefabs\\Salles");
    }

    public void BuildGroup()
    {
        List<Room> salles = new List<Room>();
        foreach (Room r in sallePossible)
        {
                print("ajoute");
                salles.Add(r);
        }
        foreach (GameObject g in groupedRooms)
        {
            int index = Random.Range(0, sallePossible.Length-1);
            print(index);
            print(sallePossible.Length);
            Instantiate(sallePossible[index].gameObject, g.transform.position, g.transform.rotation,g.transform);
        }
    }
}
