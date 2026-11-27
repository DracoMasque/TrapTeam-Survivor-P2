using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private float damage;
    private GameObject target;
    
    List<GameObject> targets = new List<GameObject>();

    private Cooldown timer;
    [SerializeField] private Animation dottedCircle;
    [SerializeField] private GameObject cooldownSprite;
    
    public void Init(GameObject _target, float _damage, float _magicCooldown)
    {
        target = _target;
        damage = _damage;
        
        timer = GetComponent<Cooldown>();
        timer.OnTimeout = DoThunder;
        
        if(timer) timer.maxCooldownTime = _magicCooldown;
        else print(timer);
        StartCooldown();
        dottedCircle.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target.tag))
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(target.tag))
        {
            targets.Remove(other.gameObject);
        }
    }
    void DoThunder()
    {
        print("Thunder");
        if (target.CompareTag("Player"))
        {
            Controlleur.instance.currentHealth -= damage;
        }
        else if (target.CompareTag("Enemy"))
        {
            foreach (var t in targets)
            {
                t.GetComponent<EnemyPrefab>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    Tween timerTween;
    void StartCooldown()
    {
        timer.Play();
        if (timerTween != null)
        {
            timerTween.Kill();
        }
        timerTween = cooldownSprite.transform.DOScale(new Vector3(0.2f,0.2f,0), timer.maxCooldownTime);
        timerTween.SetEase(Ease.Linear);
    }
}
