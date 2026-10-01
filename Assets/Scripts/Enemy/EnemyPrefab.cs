using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxHealth = 10;
    float currentHealth;
    [SerializeField] int attack = 10;
    public float speed;
    public float maxSpeed = 10;
    [SerializeField] float range;
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
        speed = maxSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlleur = player.GetComponent<Controlleur>();
        timer = GetComponent<Cooldown>();
        animator = GetComponent<Animator>();
        animationComponent = GetComponent<Animation>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
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
        transform.position = Vector2.MoveTowards(transform.position, player_pos, speed * Time.deltaTime);
    }

    
    //====================ATTACK====================//
    
    //Comment l'enemy attaque le joueur
    private void Attack()
    {
        if (timer.finished && IsClose())
        {
            animator.SetBool("Attacking", true);
            playerControlleur.currentHealth =- attack;
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
            if (xpPrefab != null)
            {
                GameObject newXp = Instantiate(xpPrefab, transform.position, transform.rotation);
                Debug.Log(newXp.name);
                //newXp.GetComponent<XpRecup>().valueXp = expToDrop;
                Debug.Log("Drop XP");
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
        if (chance <= 2)
        {
            if (materialPrefab != null)
            {
                Debug.Log(chance + " AAAAAAAHHHHH ");
                Instantiate(materialPrefab, transform.position, transform.rotation);
                Debug.Log("Drop material");
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
