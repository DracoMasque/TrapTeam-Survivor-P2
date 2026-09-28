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

    void Start()
    {
        groupementPossible = Resources.LoadAll<GameObject>("BuildingPrefabs\\Groupements");
    }

    public void StartBuilding(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            if (numberMaterial == requarieredMaterial)
            {
                buildingSystem.SetActive(true);
                camera.gameObject.SetActive(false);
                StartCoroutine(DeZoom());
                GameObject groupement = Instantiate(groupementPossible[Random.Range(0, groupementPossible.Length)], transform.position, transform.rotation);
                groupement.GetComponent<Groupement>().BuildGroup();
                building.cellIndicator = groupement;
            }
        }
    }

    IEnumerator DeZoom()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
    }
}
