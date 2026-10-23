using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ElementManager : MonoBehaviour
{
    public static ElementManager instance;
    
    public List<Room> rooms;
    public float ratioFeu;
    public int numberRoomsFeu;
    public float ratioGlace;
    public int numberRoomsGlace;
    public float ratioNature;
    public int numberRoomsNature;
    public float ratioMaudit;
    public int numberRoomsMaudit;

    void Awake()
    {
        instance = this;
    }

    private void BuildRatio()
    {
        ratioFeu = numberRoomsFeu/rooms.Count;
        ratioGlace = numberRoomsGlace / rooms.Count;
        ratioNature = numberRoomsNature/rooms.Count;
        ratioMaudit = numberRoomsMaudit / rooms.Count;
    }
    
    public void AjouterRoom(Room room)
    {
        rooms.Add(room);
        BuildRatio();
    }
    
    public void RemoveRoom(Room room)
    {
        rooms.Remove(room);
        BuildRatio();
    }

}
