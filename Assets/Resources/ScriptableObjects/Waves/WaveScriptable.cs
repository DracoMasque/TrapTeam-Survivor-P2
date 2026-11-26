using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveScriptable", menuName = "Scriptable Objects/WaveScriptable")]
public class WaveScriptable : ScriptableObject
{
    [SerializeField] public Dictionary<Elements, List<GameObject>> enemies =  new Dictionary<Elements, List<GameObject>>()
    {
        { Elements.Feu , new List<GameObject>() },
        { Elements.Glace, new List<GameObject>() },
        { Elements.Nature , new List<GameObject>() },
        { Elements.Maudit , new List<GameObject>() },
    };
    public float time = 0.0f;
    [Tooltip("how many enemies per seconds are spawning")]
    public float spawnRate = 1;
}
