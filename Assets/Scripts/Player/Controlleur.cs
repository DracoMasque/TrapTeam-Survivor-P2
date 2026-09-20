using UnityEngine;
using UnityEngine.InputSystem;

public class Controlleur : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed;
    private Vector2 moveInput;
    
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
