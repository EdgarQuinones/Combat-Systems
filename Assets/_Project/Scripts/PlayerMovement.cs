using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))] // Rigibody2D is needed for movement to work
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Vector2 _lastDirection = Vector2.right;
    public Vector2 lastDirection => _lastDirection; // Read only for other scripts

    [Header ("Movement Settings")]
    // Can edit speed in inspector
    [SerializeField] private float speed = 5.0f;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Both x & y can be -1, 0, or 1
        moveInput = context.ReadValue<Vector2>();

        // Used so the attack direciton is same as direction facing
        if (moveInput != Vector2.zero)
            _lastDirection = moveInput;
    }

    private void FixedUpdate()
    {
        // moveInput is already normalized for diagonal movement 
        rb.linearVelocity = moveInput * currentSpeed;
    }
    
    // Increase or decrease speed
    public void ApplyTemporarySpeedModifier(float modifier, float duration)
    {
        StartCoroutine(TemporarySpeedModifier(modifier, duration)); // Handles the delay
    }

    // Should add back what was previously removed
    private IEnumerator TemporarySpeedModifier(float modifier, float duration)
    {
        currentSpeed += speed * modifier; // Speed change
        yield return new WaitForSeconds(duration); // Wait before going back to original speed
        currentSpeed -= speed * modifier; // Reverse Speed change
    }

    // Remove all speed debuffs
    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
