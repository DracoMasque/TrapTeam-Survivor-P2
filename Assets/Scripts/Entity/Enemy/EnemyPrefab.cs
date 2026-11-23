using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxHealth = 10;
    public float currentHealth;
    [SerializeField] int attack = 10;
    
    public float speed;
    [HideInInspector] public float modSpeedTrap = 0;
    
    public float damage;
    [HideInInspector] public float damageUpgrade = 0;
    [HideInInspector] public float modDamage = 0;
    
    [Tooltip("in seconds")] public float damageFreq;
    [HideInInspector] public float damageFreqUpgrade = 0;
    [HideInInspector] public float modDamageFreq = 1;

    public float defense;
    [HideInInspector] public float defenseUpgrade = 0;
    [HideInInspector] public float modDefense = 0;

    [SerializeField] float range;
    [HideInInspector] public float rangeUpgrade;
    [HideInInspector] public float modRange = 0;
    
    public int xpToDrop = 1;
    
    [Header("Prefabs")] 
    private RandomDrop _randomDrop;
    [SerializeField] GameObject xpPrefab;
    [SerializeField] private ElementWeakScriptable elements;
    public Etat etat;
    [HideInInspector] public GameObject closestEnemy;

    
    private GameObject player;
    private Controlleur playerControlleur;
    
    private Cooldown timer;
    
    private Animator animator;
    private Animation animationComponent;

    private Rigidbody2D rb;
    
    private GameManager gameManager;
   private SpriteRenderer spriteRenderer;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 10;
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlleur = player.GetComponent<Controlleur>();
        timer = GetComponent<Cooldown>();
        animator = GetComponent<Animator>();
        animationComponent = GetComponent<Animation>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _randomDrop = GetComponent<RandomDrop>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    
    //====================WALKING====================//
    void Update()
    {
        if (gameManager.paused)
        {
            return;
        }

        if (etat != null && etat.GetComponent<Confusion>())
        {
            Walk(closestEnemy.transform.position);
        }
        else
        {
            Walk(player.transform.position);
        }
        Attack();
    }
    private void Walk(Vector2 player_pos)
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            player_pos, 
            (speed) * Time.deltaTime
            );
        spriteRenderer.flipX = (transform.position.x - player_pos.x > 0);

    }

    
    //====================ATTACK====================//
    
    //Comment l'enemy attaque le joueur
    private void Attack()
    {
        bool negate = false;
        if (timer.finished && IsClose())
        {
            animator.SetBool("Attacking", true);
            if (etat != null && etat.GetComponent<Confusion>())
            {
                closestEnemy.GetComponent<EnemyPrefab>().TakeDamage(damage);
                    //currentHealth -= Mathf.Ceil((attack * (1 + (damage / 100))) * (1 - (Controlleur.instance.defense / 100)));
            }
            else
            {
               foreach (Equipement e in PlayerEquiment.instance.equipements)
               {
                   if (e.GetComponent<AilesGorgone>() && e.GetComponent<AilesGorgone>().negateChance <= Random.Range(0, 101))
                   {
                       e.EquipementAction();
                       negate = true;
                       break;
                   }
               }
               if (!negate)
               {
                   playerControlleur.currentHealth -=
                       Mathf.Round((attack * (1 + (damage / 100))) * (1 - (Controlleur.instance.defense / 100)));
               } 
            }
            
            timer.Play();
        }
    }
    //Quand l'animation d'attaque est terminé, reviens a l'anim de déplacement de l'animator
    private void AttackFinished()
    {
        animator.SetBool("Attacking", false);
    }
    //Est ce que le joueur est dans le range de l'enemy
    private bool IsClose()
    {
        bool result = false;
        if (etat != null && etat.GetComponent<Confusion>())
        {
            if (Vector2.Distance(transform.position, closestEnemy.transform.position) <= range)
            {
                result = true;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= range)
            {
                result = true;
            }
        }
        return result;
    } 
    
    
    //====================DEATH====================//
    
    public void TakeDamage(float damage)
    {
        if (ElementManager.instance.elementDominant == elements.weakness)
        {
            currentHealth -= Mathf.Ceil(((damage * (1 + (playerControlleur.damage / 100))) +
                                         (ElementManager.instance.elementBonusTotal[
                                              ElementManager.instance.elementDominant] *
                                          1.50f)) * (1 - (defense / 100)));
        }
        else if (ElementManager.instance.elementDominant == elements.strength)
        {
            currentHealth -= Mathf.Ceil(((damage * (1 + (playerControlleur.damage / 100))) +
                                         (ElementManager.instance.elementBonusTotal[
                                              ElementManager.instance.elementDominant] *
                                          0.50f)) * (1 - (defense / 100)));
        }
        else
        {
            currentHealth -= Mathf.Ceil(((damage * (1 + (playerControlleur.damage / 100))) +
                                         (ElementManager.instance.elementBonusTotal[
                                             ElementManager.instance.elementDominant]))
                                        * (1 - (defense / 100)));
        }

        if (etat == null && ElementManager.instance.elementDominant != Elements.None)
        {
            GameManager.instance.GetEtats(this, ElementManager.instance.elementDominant);
        }

        if (etat != null && etat.GetComponent<Maudit>())
        {
            if (etat.GetComponent<Maudit>().propagationChance <= Random.Range(0, 101))
            {
                List<GameObject> closestEnemies = GetComponent<DetectEnemy>().enemies;
                GameObject closest = closestEnemies[0];
                for (int i = 1; i < closestEnemies.Count; i++)
                {
                    if (Vector2.Distance(closestEnemies[i].transform.position, transform.position) <
                        Vector2.Distance(closest.transform.position, transform.position))
                    {
                        closest = closestEnemies[i];
                    }
                }

                List<GameObject> alreadyTouched = new List<GameObject>();
                alreadyTouched.Add(gameObject);
                closest.GetComponent<EnemyPrefab>().TakeDamagePropagation(etat.GetComponent<Maudit>().propagationRange, 
                    Mathf.Ceil((damage * (1 + (playerControlleur.damage / 100))) / 2), etat.GetComponent<Maudit>().propagationChance, alreadyTouched);
            }
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void TakeDamageTrap(float damage, Elements element, float trapBonus)
        {
            if (element == elements.weakness)
            {
                currentHealth -= Mathf.Ceil((damage * trapBonus * 1.50f) * (1 - (defense / 100)));
            }
            else if (element == elements.strength)
            {
                currentHealth -= Mathf.Ceil((damage * trapBonus * 0.50f) * (1 - (defense / 100)));
            }
            else
            {
                currentHealth -= Mathf.Ceil((damage * trapBonus) * (1 - (defense / 100)));
            }
            
            if (etat == null)
            {
                GameManager.instance.GetEtats(this, element);
            }
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }

    public void TakeDamagePropagation(float propagationRange ,float damage, float propagationChance, List<GameObject> alreadyTouched)
    {
        
        currentHealth -= Mathf.Ceil(damage * (1 - (defense / 100)));
        
        if (propagationChance <= Random.Range(0, 101))
        {
            List<GameObject> closestEnemies = GetComponent<DetectEnemy>().enemies;
            GameObject closest = closestEnemies[0];
            for (int i = 1; i < closestEnemies.Count; i++)
            {
                if (Vector2.Distance(closestEnemies[i].transform.position, transform.position) < propagationRange &&
                    closestEnemies[i] != alreadyTouched[^1] &&
                    Vector2.Distance(closestEnemies[i].transform.position, transform.position) <
                    Vector2.Distance(closest.transform.position, transform.position))
                {
                    closest = closestEnemies[i];
                }
            }

            alreadyTouched.Add(gameObject);
            closest.GetComponent<EnemyPrefab>().TakeDamagePropagation(propagationRange, Mathf.Ceil(damage / 2), propagationChance, alreadyTouched);
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        animator.SetBool("Dead", true);
    }
    
    private void DieFinished()
    {
        GameObject drop = _randomDrop.GetRandomDrop();
        if (drop.GetComponent<XP>() != null)
        {
            drop.GetComponent<XP>().valueXp = xpToDrop;
        }
        else
        {
            xpPrefab.GetComponent<XP>().valueXp = xpToDrop;
            Instantiate(xpPrefab, transform.position, transform.rotation);
        }
        Instantiate(drop, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    public void RedoSpeedModTrap(float modTrap)
    {
        modSpeedTrap += modTrap; 
        speed += Mathf.Ceil(modSpeedTrap);
    }

    public void SpeedReturn(float modTrap)
    {
        speed += Mathf.Abs(modTrap);
    }
    
    public void RedoDamageModTrap(float modTrap)
    {
        modDamage += modTrap; 
        damage += Mathf.Ceil(modDamage);
    }

    public void DamageReturn(float modTrap)
    {
        damage += Mathf.Abs(modTrap);
    }
}
