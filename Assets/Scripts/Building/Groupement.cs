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
        print("on start "+sallePossible.Length);
        for (int i = 0; i < 10; i++)
        {
            print("test "+Random.Range(0, sallePossible.Length));
        }
        BuildGroup();
    }

    public void BuildGroup()
    {
        print("after start "+sallePossible.Length);
        List<Room> salles = new List<Room>();
        foreach (Room r in sallePossible)
        {
                print(salles.Count);
                salles.Add(r);
        }
        foreach (GameObject g in groupedRooms)
        {
            int index = Random.Range(0, sallePossible.Length);
            print(index);
            Instantiate(sallePossible[index].gameObject, g.transform.position, g.transform.rotation,g.transform);
        }
    }
}
