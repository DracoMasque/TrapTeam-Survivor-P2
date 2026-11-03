using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBuilding : MonoBehaviour
{
    public int numberMaterial;
    public int requarieredMaterial;
    [SerializeField] private GameObject buildingSystem;
    [SerializeField] private GameObject UIBuildingChoice;
    private GameObject uiPLayer;
    private GameObject uiWorldSpace;
    private BuildingChoice buildingChoice;
    [SerializeField] private CinemachineCamera camera;
    public BuildingSystem building;
    
    public TextMeshProUGUI keyToPress;
    public TextMeshProUGUI numberMaterialText;
    
    public GameObject cancelButton;
    public GameManager gameManager;
    public PointeurRecup pointeurRecup;
    

    void Start()
    {
        building = buildingSystem.GetComponent<BuildingSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiPLayer = GameObject.Find("UIPlayer");
        uiWorldSpace = GameObject.Find("UIWorldSpace");
        cancelButton = GameObject.Find("Exit");
        buildingChoice = uiWorldSpace.GetComponent<BuildingChoice>();
    }

    void Update()
    {
        if (numberMaterial == requarieredMaterial)
        {
            keyToPress.gameObject.SetActive(true);
        }
        
        numberMaterialText.text = "MATERIAL : " + numberMaterial.ToString() +  "/" + requarieredMaterial.ToString();
    }

    public void StartBuilding(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            if (numberMaterial == requarieredMaterial)
            {
                uiWorldSpace.GetComponent<Canvas>().enabled = true; 
                buildingSystem.SetActive(true);
                building.buildable = true;
                camera.gameObject.SetActive(false);
                gameManager.paused = true;
                requarieredMaterial *= 2;
                numberMaterial = 0;
                UIBuildingChoice.gameObject.SetActive(true);
                buildingChoice.BuildingChoices();
                keyToPress.gameObject.SetActive(false);
                uiPLayer.GetComponent<Canvas>().enabled = false;
                /*
                GameObject groupement = Instantiate(groupementPossible[Random.Range(0, groupementPossible.Length)], transform.position, new Quaternion(0,0,0,0));
                building.groupement = groupement.GetComponent<Groupement>();
                groupement.transform.SetParent(building.cellIndicator.transform);
                groupement.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), building.cellIndicator.transform.localRotation);
                */
            }
        }
    }

    public void StopBuilding()
    {
        uiWorldSpace.GetComponent<Canvas>().enabled = false;
        pointeurRecup.gameObject.SetActive(false);
        buildingSystem.SetActive(false);
        camera.gameObject.SetActive(true);
        cancelButton.SetActive(false);
        gameManager.paused = false;
        uiPLayer.GetComponent<Canvas>().enabled = true;
    }
    
}
