using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Controlleur : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed;
    private Vector2 moveInput;
    
    public int currentXp = 0;
    [SerializeField] private int maxXp = 10;
    public Slider xpSlider;

    public int maxHealth;
    public int currentHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput.normalized * speed;
    }

    public void Move(InputAction.CallbackContext cxt)
    {
        moveInput = cxt.ReadValue<Vector2>();
    }
    
    
}
