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
    
    [SerializeField] int materialChance; //il faut que je change ça pour remplacer avec le nouveau system
    
    [Header("Prefabs")]
    [SerializeField]  GameObject xpPrefab;
    [SerializeField]  GameObject materialPrefab;
    
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
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        DropXp();
        DropMaterial();
        animator.SetBool("Dead", true);
    }
    private void DieFinished()
    {
        Destroy(gameObject);
    }

    private void DropXp()
    {
        //max drop XP joue sur le niveau de difficulté de l'enemy, balancing blabla, pas mtn
        int maxExp = 5;
        int minExp = 0;
        int expToDrop = Random.Range(minExp, maxExp);
        for (int i = 0; i < expToDrop; i++)
        {
            if (xpPrefab)
            {
                GameObject newXp = Instantiate(xpPrefab, transform.position, transform.rotation);
                newXp.GetComponent<XpRecup>().valueXp = expToDrop;
            }
            else
            {
                Debug.LogError("xpPrefab isn't set properly");
            }
        }
    }
    private void DropMaterial()
    {
        int chance = Random.Range(0, 100);
        if (chance <= materialChance)
        {
            if (materialPrefab != null)
            {
                Instantiate(materialPrefab, transform.position, transform.rotation);
            }
            else
            {
                Debug.LogError("materialPrefab isn't set properly");
            }
            
        }
    }
    private static bool[] CreateWeighedArray(int percentage)
    {
        bool[] array = new bool[100];
        for ( int i = 0; i < percentage; i++)
        {
            array[i] = true;
        }
        return array;
    }
}
