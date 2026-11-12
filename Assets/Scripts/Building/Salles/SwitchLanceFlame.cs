using UnityEngine;

public class SwitchLanceFlame : MonoBehaviour
{
    public Cooldown cooldown;
    public GameObject autreColision;
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActivateLanceFlame;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cooldown.Play();
             
        }

        if (collision.tag == "Enemy")
        {
            cooldown.Play();
        }
    }
    void ActivateLanceFlame()
    { 
        autreColision.SetActive(!autreColision.activeSelf);
        if (autreColision.activeSelf)
        {
            cooldown.Play();
        }
    }
}
