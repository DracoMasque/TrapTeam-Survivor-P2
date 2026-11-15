using System;
using UnityEngine;

public class BoostElementPlayer : MonoBehaviour
{
    [Tooltip("in %")] [SerializeField] private float boostRatio = 50;
    private Elements roomElements;
    private float damagePlayer;
    private float speedPlayer;

    void Start()
    {
        roomElements = GetComponent<Room>().elements.elements;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Controlleur.instance.alreadyBoosted)
            {
                damagePlayer = Controlleur.instance.damage - Controlleur.instance.damage * (boostRatio / 100);
                speedPlayer = Controlleur.instance.speed - Controlleur.instance.speed * (boostRatio / 100);
            }
            else
            {
                damagePlayer = Controlleur.instance.damage;
                speedPlayer = Controlleur.instance.speed;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!Controlleur.instance.alreadyBoosted &&
                roomElements == ElementManager.instance.elementDominant)
            {
                print("boost");
                Controlleur.instance.alreadyBoosted = true;
                Controlleur.instance.RedoDamageModTrap(Controlleur.instance.damage * (boostRatio / 100));
                Controlleur.instance.RedoSpeedModTrap(Controlleur.instance.speed * (boostRatio / 100));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Controlleur.instance.alreadyBoosted)
            {
                print("stop boost");
                Controlleur.instance.alreadyBoosted = false;
                Controlleur.instance.DamageReturn(-Controlleur.instance.damage * (boostRatio / 100));
                Controlleur.instance.SpeedReturn(-Controlleur.instance.speed * (boostRatio / 100));
            }
        }
    }
}
