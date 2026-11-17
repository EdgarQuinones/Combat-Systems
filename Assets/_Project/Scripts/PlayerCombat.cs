using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header ("Attack Settings")]
    [SerializeField] private LayerMask enemyLayer;

    [Header ("Attack1")]
    [SerializeField] private float ability1Offset = 1.0f;
    [SerializeField] private float attack1Radius = 0.75f;
    [SerializeField] private float attack1Cooldown = 3.0f;
    [SerializeField] private float attack1startUpTimer = 0.15f;
    [SerializeField] private float attack1HitTimer = 0.10f;
    [SerializeField] private float attack1RecoverTimer = 0.25f;
    private float nextAttack1Time = 0.0f;
    private float totalAttackTime;

    Vector2 attackDirection;

    private void Awake()
    {
        totalAttackTime = attack1startUpTimer + attack1HitTimer + attack1RecoverTimer;
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        // Gives the ability a cooldown using Time.time rather than decrementing in Update()
        if (Time.time < nextAttack1Time)
            return;

        nextAttack1Time = Time.time + attack1Cooldown + totalAttackTime;

        Attack1StartUp();  // animation and begin slow

        Invoke(nameof(Attack1Hit), attack1startUpTimer); // read all colliders and damage

        Invoke(nameof(Attack1StartRecover), attack1startUpTimer + attack1HitTimer); // animation, still slowed, and cooldown hasnt started yet

        Invoke(nameof(Attack1Finish), totalAttackTime);
    }

    private Vector2 CalculateAttackPosition(Vector2 playerPosition, Vector2 playersLastDirection, float offset)
    {
        // Sets attack collider in front of player
        return playerPosition + (playersLastDirection * offset);
    }

    private void Attack1StartUp()
    {
        // Animation
        // Reduce movement speed 
        Debug.Log("Attack 1 START");

        // Getting last position from PlayerMovement script
        attackDirection =  playerMovement.lastDirection.normalized;
    }
    private void Attack1Hit()
    {
        // animation
        // Hit and dmg
        // still slow
        Debug.Log("Attack 1 HIT");

        // Attempts to hit any enemies within the circle (Dont forgot ALL)
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(
            CalculateAttackPosition(transform.position, attackDirection, ability1Offset),
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
    private void Attack1StartRecover()
    {
        // animation 
        // end slow
        Debug.Log("Attack 1 RECOVER");
    }

    private void Attack1Finish()
    {
        // animation 
        // end slow
        Debug.Log("Attack 1 FINISH");
    }

    private void OnDrawGizmos()
    {
        if (playerMovement == null)
            return;

        // Red circular outline to visulize attack 1
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.DrawWireSphere(
            CalculateAttackPosition(transform.position, attackDirection, ability1Offset),
            attack1Radius);
    }
}
