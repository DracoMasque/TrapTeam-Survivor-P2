using UnityEngine;
using UnityEngine.InputSystem;

public class Armurie : MonoBehaviour
{
    private bool playerIn;
    public Cooldown cooldown;
    private Collider2D _collider2D;
    private InputAction openArmurie;

    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActiveArmurie;
        openArmurie = InputSystem.actions["Construction"];
        
        _collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerIn && PlayerBuilding.instance.numberMaterial != PlayerBuilding.instance.requarieredMaterial &&
            openArmurie.triggered)
        {
            OpenArmurie();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIArmurieEquipement.instance.activateButton.SetActive(true);
            playerIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIArmurieEquipement.instance.activateButton.SetActive(false);
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
        UIArmurieEquipement.instance.cancelButton.SetActive(true);
        UIArmurieEquipement.instance.activateButton.SetActive(false);
        UIArmurieEquipement.instance.StartArmurie(this);
    }
}
