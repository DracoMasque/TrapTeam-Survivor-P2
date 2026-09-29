using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    private Camera _camera;
    private InputAction mousePos;
    public GameObject cellIndicator;
    [SerializeField] private Grid grid;
    
    PlayerBuilding playerBuilding;
    private InputAction rotateInput;
    private InputAction placeInput;
    
    public bool buildable = true;
    public Groupement groupement = null;

    void Start()
    {
        _camera = Camera.main;
        mousePos = InputSystem.actions["MousePos"];
        placeInput = InputSystem.actions["Place"];
        rotateInput = InputSystem.actions["Rotate"];
        playerBuilding = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuilding>();
    }

    void Update()
    {
        Vector3Int gridPos = grid.WorldToCell(FollowMouse());
        cellIndicator.transform.position = grid.CellToWorld(gridPos);

        if (placeInput.triggered)
        {
            PlaceGroup();
        }

        if (rotateInput.triggered)
        {
            RotateGroup();
        }
    }

    private Vector2 FollowMouse()
    {
        return _camera.ScreenToWorldPoint(mousePos.ReadValue<Vector2>());
    }
    
    public void PlaceGroup()
    {
        if (buildable && playerBuilding.groupement != null)
        {
            if (playerBuilding.groupement.transform.parent != null)
            {
                playerBuilding.groupement.transform.SetParent(null);
                playerBuilding.groupement = null;
                playerBuilding.requarieredMaterial *= 2;
            }
        }
    }

    public void RotateGroup()
    {
        if (playerBuilding.groupement != null)
        {
            playerBuilding.groupement.transform.GetChild(0).transform.Rotate(0, 0, 90f);
        }
    }
    
}
