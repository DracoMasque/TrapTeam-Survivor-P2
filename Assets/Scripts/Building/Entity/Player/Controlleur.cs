using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

using UnityEngine.UI;

public class Controlleur : MonoBehaviour
{
    public static Controlleur instance;

    public GameObject endGameScreen;
    
    private GameManager gameManager;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    
    //modificateurs sont des multiplications donc sont de base a 1, contrairement aux Upgrades qui sont +
    [Header("Stats")]
    
    public bool alreadyBoosted = false;
    
    public float speed;
    [HideInInspector]public float speedUpgrade = 0;
    [HideInInspector]public float modSpeedTrap = 0;
    [HideInInspector]public float oldModSpeedTrap = 0;
    
    public float health;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float healthUpgrade;
     public float modHealth = 0;
     public float oldModHealth = 0; 
    
    [HideInInspector] public float healthRegen = 0;
    [HideInInspector] public float healthRegenFreq = 0;
    [HideInInspector] public float modHealthRegen = 1;
    [HideInInspector] public float modHealthRegenFreq = 1;
    
    [Tooltip("in %")] public float critDamage;
    [HideInInspector] public float critDamageUpgrade = 0;
    [HideInInspector] public float modCritDamage = 1;
    
    [Tooltip("in %")] public float critChance;
    [HideInInspector] public float critChanceUpgrade = 0;
     public float modCritChance = 0;
     [HideInInspector] public float oldModCritChance = 0;

    //Chaque armes prennent comme base un certains pourcentage des degats du joueur : Fortes -> 100%, Faibles -> 40%
    public float damage;
    [HideInInspector] public float damageUpgrade = 0;
    [HideInInspector] public float modDamage = 1;
    [HideInInspector] public float oldModDamage = 0;
    //Chaque armes prennent comme base un certains pourcentage de la freq du joueur : Fortes -> 100%, Faibles -> 40%
    [Tooltip("in seconds")]
    public float damageFreq;
    [HideInInspector] public float damageFreqUpgrade = 0;
     public float modDamageFreq = 0;
    [HideInInspector] public float OldModDamageFreq = 0;

    public float defense;
    [HideInInspector] public float defenseUpgrade = 0;
    [HideInInspector] public float modDefense = 0;
    [HideInInspector] public float oldmodDefense = 0;
    
    [HideInInspector]public float currentXp = 0;
    
    [Header("UI")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        currentHealth = health;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.paused)
        {
            rb.linearVelocity = new Vector2(0, 0);
            return;
        }
        rb.linearVelocity = moveInput.normalized * speed;
        healthSlider.value = currentHealth/health;
        healthText.text = currentHealth + "/" + health;

        if (currentHealth <= 0)
        {
            endGameScreen.SetActive(true);
            gameManager.paused = true;
        }
    }

    public void Move(InputAction.CallbackContext cxt)
    {
        moveInput = cxt.ReadValue<Vector2>();
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, health);
    }

    public void RedoHealthMod()
    {
        oldModHealth = modHealth;
        modHealth = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<SangHydre>())
            {
                e.GetComponent<SangHydre>().OnMaxHealthChange(this);
            }
        }
        currentHealth += Mathf.Ceil(modHealth) - (health - currentHealth);
        health += Mathf.Ceil(modHealth);
    }
    public void RedoSpeedMod()
    {
        oldModSpeedTrap = modSpeedTrap;
        speed -= modSpeedTrap;
        modSpeedTrap = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<SabotsCentaures>())
            {
                e.GetComponent<SabotsCentaures>().OnMaxSpeedChange(this);
            }
        }
        speed += Mathf.Ceil(modSpeedTrap);
    }

    public void RedoSpeedModTrap(float modTrap)
    {
        oldModSpeedTrap = modSpeedTrap;
        speed -= modSpeedTrap;
        modSpeedTrap = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<SabotsCentaures>())
            {
                e.GetComponent<SabotsCentaures>().OnMaxSpeedChange(this);
            }
        }
        speed += Mathf.Ceil(modSpeedTrap) + modTrap;
    }

    public void SpeedReturn(float modTrap)
    {
        speed += modTrap;
    }
    
    public void RedoDamageMod()
    {
        oldModDamage = modDamage;
        damage -= modDamage;
        modDamage = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<GriffesGarous>())
            {
                e.GetComponent<GriffesGarous>().OnMaxDamageChange(this);
            }
        }
        damage += Mathf.Ceil(modDamage);
    }
    
    public void RedoDamageModTrap(float modTrap)
    {
        oldModDamage = modDamage;
        damage -= modDamage;
        modDamage = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<GriffesGarous>())
            {
                e.GetComponent<GriffesGarous>().OnMaxDamageChange(this);
            }
        }
        damage += Mathf.Ceil(modDamage) + modTrap;
    }

    public void DamageReturn(float modTrap)
    {
        damage += modTrap;
    }
    
    public void RedoDefenseMod()
    {
        oldmodDefense = modDefense;
        defense -= modDefense;
        modDefense = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<CarapaceTarasque>())
            {
                e.GetComponent<CarapaceTarasque>().OnMaxDefenseChange(this);
            }
        }
        defense += Mathf.Ceil(modDefense);
    }
    
    public void RedoCritChanceMod()
    {
        oldModCritChance = modCritChance;
        critChance -= modCritChance;
        modCritChance = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<PotionBaba>())
            {
                e.GetComponent<PotionBaba>().OnMaxCritChanceChange(this);
            }
        }
        critChance += Mathf.Ceil(modCritChance);
    }
    
    public void RedoDamageFreqMod()
    {
        OldModDamageFreq = modDamageFreq;
        damageFreq -= modDamageFreq;
        modDamageFreq = 0;
        List<Equipement> equipements = GetComponent<PlayerEquiment>().equipements;
        foreach (Equipement e in equipements)
        {
            if (e.GetComponent<EssenceVelociraptor>())
            {
                e.GetComponent<EssenceVelociraptor>().OnMaxDamageFreqChange(this);
            }
        }
        damageFreq += Mathf.Ceil(modDamageFreq);
    }
    
}
