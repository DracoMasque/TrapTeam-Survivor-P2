using System;
using System.Collections.Generic;
using UnityEngine;

public class PickupRecup : MonoBehaviour
{
    
    [SerializeField] private float speedXp = 10f;
    private List<GameObject> _pickups = new List<GameObject>();

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            _pickups.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            _pickups.Remove(other.gameObject);
        }
    }
    void Update()
    {
        foreach (var pickup in _pickups)
        {
            pickup.transform.position = Vector3.MoveTowards(pickup.transform.position,
                transform.position,
                speedXp * Time.deltaTime);
        }
    }
    

}
