using UnityEngine;

public class PlayableZone : MonoBehaviour
{
    public BuildingSystem buildingSystem;

    private void OnTriggerExit2D(Collider2D other)
    {
        print("no Room");
        if (other.gameObject.tag == "Room")
        {
            
            buildingSystem.buildable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("yes Room");
        if (other.gameObject.tag == "Room")
        {
            
            buildingSystem.buildable = true;
        }
    }
}
