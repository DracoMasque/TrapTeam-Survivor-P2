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
    private BuildingChoise buildingChoice;
    [SerializeField] private CinemachineCamera camera;
    public BuildingSystem building;
    
    public TextMeshProUGUI keyToPress;
    public TextMeshProUGUI numberMaterialText;
    
    public Button cancelButton;
    public GameManager gameManager;
    public PointeurRecup pointeurRecup;
    

    void Start()
    {
        building = buildingSystem.GetComponent<BuildingSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buildingChoice = GameObject.Find("UIBuildingSelect").GetComponent<BuildingChoise>();
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
                buildingSystem.SetActive(true);
                building.buildable = true;
                camera.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(true);
                gameManager.paused = true;
                requarieredMaterial *= 2;
                numberMaterial = 0;
                UIBuildingChoice.gameObject.SetActive(true);
                buildingChoice.BuildingChoice();
                keyToPress.gameObject.SetActive(false);
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
        print("Stop Building");
        pointeurRecup.gameObject.SetActive(false);
        buildingSystem.SetActive(false);
        camera.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(false);
        gameManager.paused = false;
    }
    
}
