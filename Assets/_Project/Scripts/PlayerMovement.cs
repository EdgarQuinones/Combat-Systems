using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private float speed = 5.0f;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Debug.Log("X Input: " + moveInput.x);
        Debug.Log("Y Input: " + moveInput.y);

        rb.linearVelocity = new Vector2(
            moveInput.x * speed,
            moveInput.y * speed);
    }
}
