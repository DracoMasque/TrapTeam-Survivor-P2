using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxHealth = 10;
    float currentHealth;
    [SerializeField] int attack = 10;
    
    public float speed;
    [HideInInspector] public float modSpeedTrap = 1;
    
    public float damage;
    [HideInInspector] public float damageUpgrade = 0;
    [HideInInspector] public float modDamage = 1;
    
    [Tooltip("in seconds")] public float damageFreq;
    [HideInInspector] public float damageFreqUpgrade = 0;
    [HideInInspector] public float modDamageFreq = 1;

    public float defense;
    [HideInInspector] public float defenseUpgrade = 0;
    [HideInInspector] public float modDefense = 1;

    [SerializeField] float range;
    [HideInInspector] public float rangeUpgrade;
    [HideInInspector] public float modRange = 1;
    
    public int xpToDrop = 1;
    
    [Header("Prefabs")] 
    private RandomDrop _randomDrop;

    private ElementWeakScriptable elements;

    
    private GameObject player;
    private Controlleur playerControlleur;
    
    private Cooldown timer;
    
    private Animator animator;
    private Animation animationComponent;
    
    private GameManager gameManager;
   
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlleur = player.GetComponent<Controlleur>();
        timer = GetComponent<Cooldown>();
        animator = GetComponent<Animator>();
        animationComponent = GetComponent<Animation>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _randomDrop = GetComponent<RandomDrop>();
    }
    
    
    //====================WALKING====================//
    void Update()
    {
        if (gameManager.paused)
        {
            return;
        }
        Walk(player.transform.position);
        Attack();
    }
    private void Walk(Vector2 player_pos)
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            player_pos, 
            (speed * modSpeedTrap) * Time.deltaTime
            );
    }

    
    //====================ATTACK====================//
    
    //Comment l'enemy attaque le joueur
    private void Attack()
    {
        if (timer.finished && IsClose())
        {
            animator.SetBool("Attacking", true);
            playerControlleur.currentHealth -= attack;
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
        if (Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            result = true;
        }
        return result;
    } 
    
    
    //====================DEATH====================//
    
    public void TakeDamage(int damage)
    {
        if (ElementManager.instance.elementDominant == elements.weakness)
        {
            currentHealth -= ((damage * (1 + (playerControlleur.damage / 100))) +
                              (ElementManager.instance.elementBonusTotal[ElementManager.instance.elementDominant] *
                               1.50f)) * (defense / 100);
        }
        else if (ElementManager.instance.elementDominant == elements.strength)
        {
            currentHealth -= ((damage * (1 + (playerControlleur.damage / 100))) +
                              (ElementManager.instance.elementBonusTotal[ElementManager.instance.elementDominant] *
                               0.50f)) * (defense / 100);
        }
        else
        {
            currentHealth -= ((damage * (1 + (playerControlleur.damage / 100))) +
                              (ElementManager.instance.elementBonusTotal[ElementManager.instance.elementDominant])) 
                              * (defense / 100);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void TakeDamageTrap(int damage, Elements element, float trapBonus)
        {
            if (element == elements.weakness)
            {
                currentHealth -= (damage * trapBonus * 1.50f) * (defense / 100);
            }
            else if (element == elements.strength)
            {
                currentHealth -= (damage * trapBonus * 0.50f) * (defense / 100);
            }
            else
            {
                currentHealth -= (damage * trapBonus) * (defense / 100);
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
}
