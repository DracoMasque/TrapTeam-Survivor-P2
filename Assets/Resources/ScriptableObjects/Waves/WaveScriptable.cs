using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveScriptable", menuName = "Scriptable Objects/WaveScriptable")]
public class WaveScriptable : ScriptableObject
{
    [SerializeField] public Dictionary<GameObject, int> enemies;
    public float time = 0.0f;
    [Tooltip("how many enemies per seconds are spawning")]
    public float spawnRate = 1;
}
