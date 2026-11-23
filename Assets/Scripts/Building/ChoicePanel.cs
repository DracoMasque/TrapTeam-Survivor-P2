using TMPro;
using UnityEngine;

public class ChoicePanel : MonoBehaviour
{
    public GameObject imagePoint;
    public TextMeshProUGUI[] roomNames;
    public TextMeshProUGUI[] elementNames;

    public void ClearRoomNames()
    {
        foreach (TextMeshProUGUI t in roomNames)
        {
            t.text = "-";
        }
        foreach (TextMeshProUGUI t in elementNames)
        {
            t.text = "-";
        }
    }
}
