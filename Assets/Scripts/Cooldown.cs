using System;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] private bool autoStart = true;
    private bool _canStart;
    [SerializeField] public bool loop = false;
    [SerializeField] public float maxCooldownTime = 2f;
    public bool finished = false;
    [HideInInspector]
    public float currentTime = 0f;

    private void Start()
    {
        currentTime = maxCooldownTime;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _canStart = autoStart;
    }

    public void Play()
    {
        _canStart = true;
        finished = false;
        currentTime = maxCooldownTime;
    }

    private void Update()
    {
        //Fait baisser le timer
        //Si il est a 0 on peut a nouveau attaquer
        //Si le timer loop il recommencer tout seule 
        if (gameManager.paused || !_canStart)
        {
            return;
        }
        currentTime -= Time.deltaTime;
        
        if (currentTime <= 0f)
        {
            finished = true;
            if (loop)
            {
                Play();
            }
        }
    }
}