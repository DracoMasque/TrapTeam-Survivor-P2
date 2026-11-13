using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AmeliorationArme : MonoBehaviour
{
    [FormerlySerializedAs("ArmeList")] public List<Arme> armeList;

    public static AmeliorationArme instance;

    void Awake()
    {
        instance = this;
    }
    
}
