using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    private Camera _camera;
    private InputAction mousePos;
    private Vector2 lastPos;
    public GameObject cellIndicator;
    [SerializeField] private Grid grid;

    void Start()
    {
        _camera = Camera.main;
        mousePos = InputSystem.actions["MousePos"];
    }

    void Update()
    {
        Vector3Int gridPos = grid.WorldToCell(FollowMouse());
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
        //print("gridPos: " + gridPos);
        //print("indicateur: " + cellIndicator.transform.position);
    }

    private Vector2 FollowMouse()
    {
        return _camera.ScreenToWorldPoint(mousePos.ReadValue<Vector2>());
    }
}
