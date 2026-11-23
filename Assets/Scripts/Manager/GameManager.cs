using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private WaveScriptable[] waves ;
    private WaveScriptable currentWave;
    private WaveScriptable nextWave;
    private Cooldown timer;
    public bool paused = false;
    
    [Range(1f, 100f)] public float etatsChance;
    [SerializeField] private Dictionary<Elements, GameObject> etats = new Dictionary<Elements, GameObject>()
    {
        { Elements.Feu, null },
        { Elements.Glace, null },
        { Elements.Nature, null },
        { Elements.Maudit, null },
    };
    
    private void Start()
    {
        timer = GetComponent<Cooldown>();
        timer.OnTimeout = UpdateWave;
        currentWave = waves[0];
        nextWave = waves[0];
        BroadcastMessage("ChangeEnemy", currentWave.enemies);

        instance = this;
    }
    
    void UpdateWave()
    {
        if (paused) return;

        if (timer.currentTime >= nextWave.time)
        {
            currentWave = nextWave;
            NextWave();
            if (currentWave.enemies.Count > 0)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                BroadcastMessage("ChangeEnemy", currentWave.enemies);
            }
        }
    }
    private void NextWave()
    {
        WaveScriptable newWave = currentWave;
        newWave.time = timer.maxCooldownTime;
        
        foreach (WaveScriptable wave in waves)
        {
            float previousWave = wave.time;
            if (wave.time > nextWave.time && wave.time < previousWave)
            {
                newWave = wave;
            }
        }
        
        nextWave = newWave;
    }
    
    public void GetEtats(EnemyPrefab enemy, Elements element)
    {
        if (etatsChance <= Random.Range(0f, 101f) && ElementManager.instance.elementLevels[element] > 0)
        {
            GameObject etatInstace = Instantiate(etats[element], enemy.transform, true);
            enemy.etat = etatInstace.GetComponent<Etat>();
            etatInstace.GetComponent<Etat>().StartEtat();
        }
    }
}
