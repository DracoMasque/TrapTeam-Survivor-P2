using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask placementLayerMask;
    private InputAction mousePos;
    private Vector2 lastPos;
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private Grid grid;

    void Start()
    {
        _camera = Camera.main;
        mousePos = InputSystem.actions["MousePos"];
    }

    void Update()
    {
        mouseIndicator.transform.position = FollowMouse();
        Vector3Int gridPos = grid.WorldToCell(FollowMouse());
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }

    private Vector2 FollowMouse()
    {
        /*
        Vector3 mousePos3 = Mouse.current.position.ReadValue();
        mousePos3.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos3);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1, placementLayerMask))
        {
            lastPos = hit.point;
            //print(lastPos);
        }
        */
        
        return _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
