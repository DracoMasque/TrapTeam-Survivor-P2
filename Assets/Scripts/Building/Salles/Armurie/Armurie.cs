using UnityEngine;
using UnityEngine.InputSystem;

public class Armurie : MonoBehaviour
{
    private bool playerIn;
    public Cooldown cooldown;
    private Collider2D _collider2D;
    public GameObject activateButton;
    private InputAction openArmurie;
    [SerializeField] private GameObject cancelButton;

    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActiveArmurie;
        openArmurie = InputSystem.actions["Construction"];
        cancelButton = GameObject.Find("Cancel");
        activateButton = GameObject.Find("ActivateButton");
        
        _collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerIn && PlayerBuilding.instance.numberMaterial != PlayerBuilding.instance.requarieredMaterial &&
            openArmurie.triggered)
        {
            activateButton.SetActive(false);
            OpenArmurie();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activateButton.SetActive(true);
            playerIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activateButton.SetActive(false);
            playerIn = false;
        }
    }

    void ActiveArmurie()
    {
        _collider2D.enabled = true;
    }

    public void UsedArmurie()
    {
        _collider2D.enabled = false;
    }

    void OpenArmurie()
    {
        GameManager.instance.paused = true;
        cancelButton.SetActive(true);
        UIArmurieEquipement.instance.StartArmurie(this);
    }
}
