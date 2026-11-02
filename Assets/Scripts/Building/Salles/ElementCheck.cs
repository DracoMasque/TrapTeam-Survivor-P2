using System;
using UnityEngine;

public class ElementCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Room")
        {
            SendMessageUpwards("ChangeBonusElement", other.GetComponent<Room>().elements.elements);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Room")
        {
            SendMessageUpwards("RetireChangeBonusElement", other.GetComponent<Room>().elements.elements);
        }
    }
}
