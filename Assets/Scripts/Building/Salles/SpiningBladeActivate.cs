using UnityEngine;

public class SpiningBladeActivate : MonoBehaviour
{
    public Cooldown cooldown;
    public GameObject spiningBlade;
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActivatePique;
    }
    
    void ActivatePique()
    {
        spiningBlade.SetActive(!spiningBlade.activeSelf);
        transform.Rotate(new Vector3(0, 0, 90));
        cooldown.Play();
    }
}
