using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header ("Attack Settings")]
    [SerializeField] private float ability1Offset = 1.0f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attack1Radius = 0.75f;
    [SerializeField] private float attack1Cooldown = 3.0f;
    [SerializeField] private float attack1startUp = 0.15f;
    [SerializeField] private float attack1AttackStart = 0.10f;
    [SerializeField] private float attack1Recover = 0.25f;
    private float nextAttack1Time = 0.0f;

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        // Gives the ability a cooldown using Time.time rather than decrementing in Update()
        if (Time.time < nextAttack1Time)
            return;
        nextAttack1Time = Time.time + attack1Cooldown;

        // invoke (startup, attack1startUp) // animation and begin slow
        // invoke (attack, attack1startUp + attack1AttackStart) // read all colliders and damage
        // invoke (recover, attack1startUp + attack1AttackStart + attack1Recover) // animation, still slowed, and cooldown hasnt started yet

        // Attempts to hit any enemies within the circle (Dont forgot ALL)
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(
            CalculateAttackPosition(transform.position, playerMovement.lastDirection, ability1Offset),
            attack1Radius,
            enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            Health enemyHealth = enemy.GetComponent<Health>(); // Get enemy health component

            if (enemyHealth != null)
            {
                enemyHealth.takeDamage(); // Enemy should take 10 health by default

                Debug.Log("Player hit {" + enemy.name + "} with attack 1. Enemy now has " + enemyHealth.currentHealth + " health.");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerMovement == null)
            return;

        // Red circular outline to visulize attack 1
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.DrawWireSphere(
            CalculateAttackPosition(transform.position, playerMovement.lastDirection, ability1Offset), 
            attack1Radius);
    }

    private Vector2 CalculateAttackPosition(Vector2 playerPosition, Vector2 playersLastDirection, float offset)
    {
        // Getting last position from PlayerMovement script
        Vector2 lastDirection = playersLastDirection.normalized;

        // Sets attack collider in front of player
        return playerPosition + (lastDirection * offset);
    }
}
