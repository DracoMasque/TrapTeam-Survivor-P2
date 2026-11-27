using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [SerializeField] private Cooldown timer;
    [Header("Stats")]
    [SerializeField] float maxHealth = 10;
    public float currentHealth;
    public int attack;
    public ElementWeakScriptable elementBoss;
    public float speed;
    
    private SpriteRenderer spriteRenderer;
    protected GameObject player;
    protected Room bossRoom;
    
    private Rigidbody2D rb;
    private Animator _animator;
    
    private Animator animator;

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Controlleur.instance.gameObject;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        bossRoom = GetComponentInParent<Room>();
        elementBoss = bossRoom.elements;
        _animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        
        Walk(player.transform.position);
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

    public void TakeDamage(float damage, Elements element)
    {
        if (element == elementBoss.weakness)
        {
            currentHealth -= Mathf.Ceil(damage * 1.50f);
        }
        else if (element == elementBoss.strength)
        {
            currentHealth -= Mathf.Ceil(damage * 0.50f);
        }
        else
        {
            currentHealth -= Mathf.Ceil(damage);
        }

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            transform.position = Vector2.zero;
            SendMessageUpwards("BossDied");
        }
    }
    
    public void Revive()
    {
        currentHealth = maxHealth * bossRoom.bonusElement;
        gameObject.SetActive(true);
    }
}
