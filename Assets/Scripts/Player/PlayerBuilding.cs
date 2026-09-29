using System.Collections;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilding : MonoBehaviour
{
    public int numberMaterial;
    public int requarieredMaterial;
    [SerializeField] private GameObject buildingSystem;
    [SerializeField] private CinemachineCamera camera;
    private GameObject[] groupementPossible;
    public BuildingSystem building;
    public GameObject groupement;
    

    void Start()
    {
        groupementPossible = Resources.LoadAll<GameObject>("BuildingPrefabs\\Groupements");
        building = buildingSystem.GetComponent<BuildingSystem>();
    }
    

    public void StartBuilding(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            if (numberMaterial == requarieredMaterial)
            {
                buildingSystem.SetActive(true);
                building.buildable = true;
                camera.gameObject.SetActive(false);
                groupement = Instantiate(groupementPossible[Random.Range(0, groupementPossible.Length)], transform.position, new Quaternion(0,0,0,0));
                building.groupement = groupement.GetComponent<Groupement>();
                groupement.transform.SetParent(building.cellIndicator.transform);
                groupement.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), building.cellIndicator.transform.localRotation);
            }
        }
    }
    
}
