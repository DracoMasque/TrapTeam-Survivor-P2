using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    [SerializeField] int attack = 10;
    [SerializeField] int speed = 10;
    [SerializeField] float range;
    private GameObject player;
    private Controlleur playerControlleur;
    
    private AttaqueCooldown timer;
    
    private Animator animator;
    private Animation animationComponent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlleur = player.GetComponent<Controlleur>();
        timer = GetComponent<AttaqueCooldown>();
        animator = GetComponent<Animator>();
        animationComponent = GetComponent<Animation>();
    }

    void Update()
    {
        Walk(player.transform.position);
        Attack();
        
    }
    void Attack()
    {
        if (timer.canAttack && IsClose())
        {
            //Animation, ou alors je deplace les degats dans une autre fonction 
            //pour qu'on puisse sync a partir d'un AnimationEvent
            animator.SetBool("Attacking", true);
            playerControlleur.currentHealth =- attack;
            timer.Play();
        }
    }
    //Pour l'animation event
    void AttackFinished()
    {
        animator.SetBool("Attacking", false);
    }
    void Walk(Vector2 player_pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, player_pos, speed * Time.deltaTime);
    }
    bool IsClose()
    {
        bool result = false;
        if (Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            result = true;
        }
        return result;
    }

}
