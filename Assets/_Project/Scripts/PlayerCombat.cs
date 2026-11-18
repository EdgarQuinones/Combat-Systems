using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header ("Attack Settings")]
    [SerializeField] private LayerMask enemyLayer;

    [Header ("Attack 1")]
    [SerializeField] private float ability1Offset = 1.0f;
    [SerializeField] private float attack1Radius = 0.75f;
    [SerializeField] private float attack1Cooldown = 3.0f;
    [SerializeField] private float attack1startUpTimer = 0.15f;
    [SerializeField] private float attack1HitTimer = 0.10f;
    [SerializeField] private float attack1RecoverTimer = 0.25f;
    [SerializeField] private float attack1SpeedChange = -0.5f;
    [SerializeField] private float attack1KnockBackStrength = 2.0f;
    private float nextAttack1Time = 0.0f;
    private float totalAttackTime;

    Vector2 attackDirection;
    Vector2 attackPosition;

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
        playerMovement.ApplyTemporarySpeedModifier(attack1SpeedChange, totalAttackTime);
        attackDirection =  playerMovement.lastDirection.normalized;
        attackPosition = CalculateAttackPosition(transform.position, attackDirection, ability1Offset);
    }

    private void Attack1Hit()
    {
        // animation
        // Hit and dmg
        // still slow
        Debug.Log("Attack 1 HIT");

        // Attempts to hit any enemies within the circle (Dont forgot ALL)
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(
            attackPosition,
            attack1Radius,
            enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            Health enemyHealth = enemy.GetComponent<Health>(); // Dmg, death, etc
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>(); // Knockbacks, etc

            // If enemy is a living entity, take damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(); // Enemy should take 10 health by default
                //Debug.Log("Player hit {" + enemy.name + "} with attack 1. Enemy now has " + enemyHealth.currentHealth + " health.");
            }

            // If enemy can move, knock enemy back
            if (enemyMovement != null)
            {
                enemyMovement.ApplyForce(attackDirection * attack1KnockBackStrength); // Apply knockback
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
            playerMovement.lastDirection + (Vector2)transform.position,
            attack1Radius);
    }
}
