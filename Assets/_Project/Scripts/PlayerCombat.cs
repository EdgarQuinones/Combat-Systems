using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header ("Attack Settings")]
    [SerializeField] private float offset = 2.0f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attack1Radius = 5.0f;
    private Vector2 attack1Position;

    private Vector2 lastDirection;


    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Debug.Log("Attack 1");

        // Attempts to hit any enemies within the circle
        Collider2D hit = Physics2D.OverlapCircle(
            attack1Position,
            attack1Radius,
            enemyLayer);
    }

    private void Update()
    {
        // Getting last position from PlayerMovement script
        lastDirection = playerMovement.lastDirection.normalized;

        // Sets attack collider in front of player (Local since its child)
        attack1Position = lastDirection.normalized * offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.DrawSphere(attack1Position, attack1Radius);
    }
}
