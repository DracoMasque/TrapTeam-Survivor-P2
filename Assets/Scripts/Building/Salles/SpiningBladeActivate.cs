using UnityEngine;

public class SpiningBladeActivate : MonoBehaviour
{
    public Cooldown cooldown;
    public GameObject spiningBlade;
    private GameManager gameManager;
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActivatePique;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void ActivatePique()
    {
        if (gameManager.paused)
        {
            return;
        }
        spiningBlade.SetActive(!spiningBlade.activeSelf);
        transform.Rotate(new Vector3(0, 0, 90));
        cooldown.Play();
    }
}
