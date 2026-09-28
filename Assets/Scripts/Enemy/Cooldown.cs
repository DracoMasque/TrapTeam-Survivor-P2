using System;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] public bool loop = false;
    [SerializeField] private float maxCooldownTime = 2f;
    public bool finished = false;
    float _currentTime = 0f;

    public void Start()
    {
        _currentTime = maxCooldownTime;
    }

    public void Play()
    {
        finished = false;
        _currentTime = maxCooldownTime;
    }

    void Update()
    {
        //Fait baisser le timer
        //Si il est a 0 on peut a nouveau attaquer
        //Si le timer loop il recommencer tout seule 
        _currentTime -= Time.deltaTime;
        
        if (_currentTime <= 0f)
        {
            finished = true;
            if (loop)
            {
                Play();
            }
        }
    }
}