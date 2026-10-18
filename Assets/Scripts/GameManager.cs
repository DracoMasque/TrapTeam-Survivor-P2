using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveScriptable[] waves ;
    private WaveScriptable currentWave;
    private WaveScriptable nextWave;
    private Cooldown timer;
    public bool paused = false;
    
    private void Start()
    {
        timer = GetComponent<Cooldown>();
        currentWave = waves[0];
        nextWave = waves[0];
        BroadcastMessage("ChangeEnemy", currentWave.enemies);
    }

    private void Update()
    {
        if (paused)
        {
            return;
        }
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
}
