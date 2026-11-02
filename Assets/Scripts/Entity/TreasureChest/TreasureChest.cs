using System;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player collided with " + other.name);
            UiChest.instance.EnableChest();
        }
    }
}
