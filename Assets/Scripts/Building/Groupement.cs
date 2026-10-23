using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Groupement : MonoBehaviour
{
    public List<GameObject> groupedRooms;
    [SerializeField] private Room[] sallePossible;
    public bool placed;
    private BuildingSystem buildingSystem;
    public Vector3 previousPosition;

    private void Start()
    {
        buildingSystem = GameObject.FindGameObjectWithTag("BuildingSystem").GetComponent<BuildingSystem>();
    }

    public void BuildGroup(List<Room> rooms)
    {
        
        for (int i = 0; i < groupedRooms.Count; i++)
        {
            Instantiate(rooms[i].gameObject, groupedRooms[i].transform.position, groupedRooms[i].transform.rotation,groupedRooms[i].transform);
        }
    }

    public void BuildThumbnail(List<Room> rooms)
    {
        for (int i = 0; i < groupedRooms.Count; i++)
        {
            Instantiate(AssetPreview.GetMiniThumbnail(rooms[i].gameObject), groupedRooms[i].transform.position, groupedRooms[i].transform.rotation, groupedRooms[i].transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Groupement")
        {
            print("!buildable");
            buildingSystem.buildable = false;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Groupement")
        {
            buildingSystem.buildable = true;
        }
    }
}
