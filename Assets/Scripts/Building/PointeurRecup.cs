using System;
using UnityEngine;

public class PointeurRecup : MonoBehaviour
{
    public bool canPickUp = false;
    public BuildingSystem building;
    public Groupement groupement = null;

    private void Update()
    {
        gameObject.transform.position = building.FollowMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Groupement" && other.gameObject.GetComponent<Groupement>().placed)
        {
            print("Groupement entered");
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Groupement")
        {
            print("Groupement exited");
            canPickUp = false;
        }
    }
    
    public void PickUpGroup()
    {
        if (canPickUp)
        {
            groupement.transform.SetParent(building.cellIndicator.transform);
            groupement.placed = false;
            canPickUp = false;
        }
    }
}
