
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiqueDesactive : MonoBehaviour
{
    public Cooldown cooldown;
   public GameObject autreColision;
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActivatePique;
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
    void ActivatePique()
    { 
        autreColision.SetActive(!autreColision.activeSelf);
        if (autreColision.activeSelf)
        {
            cooldown.Play();
        }
    }
}