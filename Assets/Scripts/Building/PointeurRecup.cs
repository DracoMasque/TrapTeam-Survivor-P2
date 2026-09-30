using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointeurRecup : MonoBehaviour
{
    public bool canPickUp = false;
    public BuildingSystem building;
    public Groupement groupement = null;
    private InputAction placeInput;
    private InputAction mousePos;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        mousePos = InputSystem.actions["MousePos"];
        placeInput = InputSystem.actions["Place"];
        building = building.GetComponent<BuildingSystem>();
    }

    private void Update()
    {
        gameObject.transform.position = FollowMouse();

        if (placeInput.triggered)
        {
            PickUpGroup();
        }
    }
    
    private Vector2 FollowMouse()
    {
        return _camera.ScreenToWorldPoint(mousePos.ReadValue<Vector2>());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Groupement" && other.gameObject.GetComponent<Groupement>().placed)
        {
            canPickUp = true;
            groupement = other.gameObject.GetComponent<Groupement>();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Groupement")
        {
            canPickUp = false;
            groupement = null;
        }
    }
    
    public void PickUpGroup()
    {
        if (canPickUp)
        {
            print("Picking up group");
            groupement.transform.SetParent(building.cellIndicator.transform);
            building.groupement = groupement;
            groupement.placed = false;
            canPickUp = false;
            gameObject.SetActive(false);
        }
    }
}
