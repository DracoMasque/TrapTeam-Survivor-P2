using System;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] public bool loop = false;
    [SerializeField] public float maxCooldownTime = 2f;
    public bool finished = false;
    public float currentTime = 0f;

    private void Start()
    {
        currentTime = maxCooldownTime;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Play()
    {
        finished = false;
        currentTime = maxCooldownTime;
    }

    private void Update()
    {
        //Fait baisser le timer
        //Si il est a 0 on peut a nouveau attaquer
        //Si le timer loop il recommencer tout seule 
        if (gameManager.paused)
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