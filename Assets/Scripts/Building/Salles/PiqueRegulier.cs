using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiqueRegulier : MonoBehaviour
{
    public List<GameObject> piques;
    private List<Animation> piqueAnimations = new List<Animation>();
    public List<GameObject> piques2;
    private List<Animation> pique2Animations =  new List<Animation>();
    public Cooldown cooldown;

    private bool active2 = false;
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout += ActivatePique;
        foreach (GameObject pique in piques)
        {
            foreach (Animation p in pique.GetComponentsInChildren<Animation>())
            {
                piqueAnimations.Add(p);
            }
        }
        foreach (GameObject pique in piques2)
        {
            foreach (Animation p in pique.GetComponentsInChildren<Animation>())
            {
                pique2Animations.Add(p);
            }
        }
        cooldown.Play();
    }
    
    void ActivatePique()
    {
        foreach (Animation p in piqueAnimations)
        {
            if (active2)
            {
                p.Play("Spikes_Attack");
            }
            else
            {
                p.Play("Spikes_Idle");
            }
        }

        foreach (Animation p in pique2Animations)
        {
            if (!active2)
            {
                p.Play("Spikes_Attack");
            }
            else
            {
                p.Play("Spikes_Idle");
            }
        }
        cooldown.Play();
        active2 = !active2;
    }
    
}
