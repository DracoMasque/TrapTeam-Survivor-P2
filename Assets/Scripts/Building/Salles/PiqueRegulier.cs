using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiqueRegulier : MonoBehaviour
{
    public List<GameObject> pique;
    public List<GameObject> pique2;
    public Cooldown cooldown;
   
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout = ActivatePique;
    }
    
    void ActivatePique()
    {
        foreach (GameObject p in pique)
        {
            p.SetActive(!p.activeSelf);
        }

        foreach (GameObject p in pique2)
        {
            p.SetActive(!p.activeSelf);
        }
        cooldown.Play();
    }
}
