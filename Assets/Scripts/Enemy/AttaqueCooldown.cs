using UnityEngine;

public class AttaqueCooldown : MonoBehaviour
{
    [SerializeField] public bool loop = false;
    [SerializeField] private float maxCooldownTime = 2f;
    public bool canAttack = false;
    float _currentTime = 0f;

    public void Play()
    {
        canAttack = false;
        _currentTime = maxCooldownTime;
    }

    void Update()
    {
        //Fait baisser le timer
        //Si il est a 0 on peut a nouveau attaquer
        //Si le timer loop il commencer tout seule a recommencer
        _currentTime -= Time.deltaTime;
        
        if (_currentTime <= 0f)
        {
            canAttack = true;
            if (loop)
            {
                Play();
            }
        }
    }
}