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
    [SerializeField] private CinemachineCamera camera;
    private GameObject[] groupementPossible;
    public BuildingSystem building;
    public GameObject groupement;
    
    public TextMeshProUGUI keyToPress;
    public TextMeshProUGUI numberMaterialText;
    
    public Button cancelButton;
    public GameManager gameManager;
    public PointeurRecup pointeurRecup;
    

    void Start()
    {
        groupementPossible = Resources.LoadAll<GameObject>("BuildingPrefabs\\Groupements");
        building = buildingSystem.GetComponent<BuildingSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                groupement = Instantiate(groupementPossible[Random.Range(0, groupementPossible.Length)], transform.position, new Quaternion(0,0,0,0));
                building.groupement = groupement.GetComponent<Groupement>();
                groupement.transform.SetParent(building.cellIndicator.transform);
                groupement.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), building.cellIndicator.transform.localRotation);
                keyToPress.gameObject.SetActive(false);
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
