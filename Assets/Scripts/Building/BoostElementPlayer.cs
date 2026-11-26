using System;
using UnityEngine;

public class BoostElementPlayer : MonoBehaviour
{
    [Tooltip("in %")] [SerializeField] private float boostRatio = 50;
    private Elements roomElements;

    void Start()
    {
        roomElements = GetComponent<Room>().elements.elements;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ElementManager.instance.elementDominant != Elements.None && other.tag == "Player")
        {
            if (!Controlleur.instance.alreadyBoosted)
            {
                Controlleur.instance.damageBaseWhenBoosted = Controlleur.instance.damage;
                Controlleur.instance.speedBaseWhenBoosted = Controlleur.instance.speed;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (ElementManager.instance.elementDominant != Elements.None && other.tag == "Player")
        {
            if (!Controlleur.instance.alreadyBoosted &&
                roomElements == ElementManager.instance.elementDominant)
            {
                Controlleur.instance.alreadyBoosted = true;
                Controlleur.instance.RedoDamageModTrap(
                    (Controlleur.instance.damageBaseWhenBoosted - Controlleur.instance.modDamage) * (boostRatio / 100));
                Controlleur.instance.RedoSpeedModTrap(
                    (Controlleur.instance.speedBaseWhenBoosted - Controlleur.instance.modSpeedTrap) * (boostRatio / 100));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Controlleur.instance.alreadyBoosted)
            {
                Controlleur.instance.alreadyBoosted = false;
                Controlleur.instance.DamageReturn(
                    -((Controlleur.instance.damageBaseWhenBoosted - Controlleur.instance.modDamage) *
                      (boostRatio / 100)));
                Controlleur.instance.SpeedReturn(
                    -((Controlleur.instance.speedBaseWhenBoosted - Controlleur.instance.modSpeedTrap) *
                      (boostRatio / 100)));
            }
        }
    }
}
