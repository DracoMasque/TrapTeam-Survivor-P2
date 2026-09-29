using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Groupement : MonoBehaviour
{
    [SerializeField] private List<GameObject> groupedRooms;
    [SerializeField] private Room[] sallePossible;
    public bool placed;

    private void Start()
    {
        sallePossible = Resources.LoadAll<Room>("BuildingPrefabs\\Salles");
        BuildGroup();
    }

    public void BuildGroup()
    {
        List<Room> salles = new List<Room>();
        foreach (Room r in sallePossible)
        {
                salles.Add(r);
        }
        foreach (GameObject g in groupedRooms)
        {
            int index = Random.Range(0, sallePossible.Length);
            Instantiate(sallePossible[index].gameObject, g.transform.position, g.transform.rotation,g.transform);
        }
    }
}
