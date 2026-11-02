using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JaugeElement : MonoBehaviour
{
    [SerializeField] public Dictionary<Elements, Slider> elementJauge = new Dictionary<Elements, Slider>
    {
        {Elements.Feu, null},
        {Elements.Glace, null},
        {Elements.Nature, null},
        {Elements.Maudit, null}
    };
    
    public static JaugeElement instance;

    void Start()
    {
        instance = this;
        foreach (KeyValuePair<Elements, Slider> i in elementJauge)
        {
            i.Value.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        RefreshJauge(elementJauge.Keys.ToArray());
    }

    public void RefreshJauge(Elements[] elements)
    {
        foreach (Elements e in elements)
        {
            elementJauge[e].value = ElementManager.instance.elementRatios[e];
            if (ElementManager.instance.elementRatios[e] == 4)
            {
                elementJauge[e].GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
            }
        }
    }
}
