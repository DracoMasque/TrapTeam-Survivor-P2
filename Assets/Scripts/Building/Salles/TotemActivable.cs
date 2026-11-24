using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TotemActivable : MonoBehaviour
{
    private Cooldown cooldown;
    public int degatTotem;
    private List<GameObject> entite = new List<GameObject>();
    
    [SerializeField] private GameObject cooldownUI;
    [SerializeField] private GameObject dottedCircle;
    [SerializeField] private Animation totem;
    private Totem totemScript;
    
    private Tween cooldownUITween;
    private bool isPlaying;
    
    void Start()
    {
        cooldown = GetComponent<Cooldown>();
        cooldown.OnTimeout += AnimateTotem;
        totemScript = totem.gameObject.GetComponent<Totem>();
        
        totemScript.OnAnimationEvent += DegatTotem;
        totemScript.OnAnimationEnded += EndAnimationTotem;
    }
    
    
    void OnTriggerStay2D (Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Enemy"))
        {
            return;
        }
        if (entite.Count > 0 && cooldown.finished)
        {
            if (!isPlaying)
            {
                cooldown.Play();
                StartCooldownUITween();
                dottedCircle.SetActive(true);
            }
        }
        if (!entite.Contains(collision.gameObject))
        {
            entite.Add(collision.gameObject);
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        entite.Remove(collision.gameObject);
        
    }

    void AnimateTotem()
    {
        totem.Play();
        ResetCooldownUITween();
    }
    void EndAnimationTotem()
    {
        if (entite.Count > 0)
        {
            cooldown.Play();
            StartCooldownUITween();
        }
    }
    
    void DegatTotem()
    {
        if (entite.Count < 1) dottedCircle.SetActive(false);
        foreach (GameObject p in entite)
        {
             /*if (p.tag == "Player")
             {
                 //p.GetComponent<Controlleur>().currentHealth -= degatTotem;
             }
            
             if (p.tag == "Enemy")
             {
                 Room parentRoom = GetComponentInParent<Room>();
                 p.GetComponent<EnemyPrefab>().TakeDamageTrap(degatTotem, parentRoom.elements.elements, parentRoom.bonusElement);
             }
        }
       
    }

    void StartCooldownUITween()
    {
        if (cooldownUITween != null)
        {
            cooldownUITween.Kill();
        }
        cooldownUITween = cooldownUI.transform.DOScale(new Vector3(1, 1, 1), cooldown.maxCooldownTime);
        cooldownUITween.SetEase(Ease.Linear);
        cooldownUITween.onComplete += ResetCooldownUITween;
    }
    void ResetCooldownUITween()
    {
        if (cooldownUITween != null)
        {
            cooldownUITween.Kill();
        }
        cooldownUI.transform.localScale = Vector3.zero;
    }
}
