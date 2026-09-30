using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

using UnityEngine.UI;

public class Controlleur : MonoBehaviour
{
    private GameManager gameManager;
    
    private Rigidbody2D rb;
    
    public float speed;
    private Vector2 moveInput;
    
    public int currentXp = 0;
    [SerializeField] public int maxXp = 10;
    public Slider xpSlider;

    public int maxHealth;
    public int currentHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xpSlider = GameObject.Find("XpBar").GetComponent<Slider>();
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        xpSlider.value = currentXp/maxXp;
        healthSlider.value = currentHealth/maxHealth;
    }

    public void Move(InputAction.CallbackContext cxt)
    {
        moveInput = cxt.ReadValue<Vector2>();
    }
    
    
}
