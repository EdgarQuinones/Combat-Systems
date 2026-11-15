using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] // Rigibody2D is needed for movement to work
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header ("Movement Settings")]
    // Can edit speed in inspector
    [SerializeField] private float speed = 5.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Both x & y can be -1, 0, or 1
        moveInput = context.ReadValue<Vector2>(); 
    }

    private void FixedUpdate()
    {
        // moveInput is already normalized for diagonal movement 
        rb.linearVelocity = moveInput * speed; 
    }
}
