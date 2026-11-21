using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameManager gameManager;

    [Header ("Attack Settings")]
    [SerializeField] private LayerMask enemyLayer;

    Vector2 attackDirection;

    [Header ("Attack 1")]
    [SerializeField] private float ability1Offset = 1.0f;
    [SerializeField] private float attack1Radius = 0.75f;
    [SerializeField] private float attack1Cooldown = 0f;
    [SerializeField] private float attack1startUpTimer = 0.15f;
    [SerializeField] private float attack1HitTimer = 0.10f;
    [SerializeField] private float attack1RecoverTimer = 0.25f;
    [SerializeField] private float attack1SpeedChange = -0.5f;
    [SerializeField] private float attack1KnockBackStrength = 2.0f;
    [SerializeField] private float attack1HitStopDuration = 0.08f;
    private HashSet<Collider2D> enemiesAlreadyHit = new(); 
    private bool canAttack1 = false;
    private float nextAttack1Time = 0.0f;
    private float totalAttackTime;


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
        Debug.Log("Attack 1 STARTUP");

        // Getting last position from PlayerMovement script
        playerMovement.ApplyTemporarySpeedModifier(attack1SpeedChange, totalAttackTime);
        attackDirection =  playerMovement.lastDirection.normalized;
    }

    private void Attack1Hit()
    {
        canAttack1 = true;
    }

    private void FixedUpdate()
    {
        // Checks collision area while true, which becomes true when player clicks it
        if (canAttack1)
        {
            // Get current enemies in range
            Collider2D[] currentEnemies = Physics2D.OverlapCircleAll(
              CalculateAttackPosition(transform.position, attackDirection, ability1Offset),
              attack1Radius,
              enemyLayer);

            foreach (Collider2D enemy in currentEnemies)
            {
                Debug.Log("target FOUND");
                if (enemiesAlreadyHit.Add(enemy)) // If it does add en enemy, then dmg them
                {
                    ApplyAttack1Damage(enemy);
                    Debug.Log("target DAMAGED");
                } else
                {
                    Debug.Log("target duplicate");
                }
            }
        }
    }

    private void ApplyAttack1Damage(Collider2D enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>(); // Dmg, death, etc
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>(); // Knockbacks, etc

        // If enemy is a living entity, take damage
        if (enemyHealth != null)
        {
            // Both use same method as they have same parent
            enemyHealth.TakeDamage(); // Enemy should take 10 health by default
                                      //Debug.Log("Player hit {" + enemy.name + "} with attack 1. Enemy now has " + enemyHealth.currentHealth + " health.");
        }

        // If enemy can move, knock enemy back
        if (enemyMovement != null)
        {
            enemyMovement.ApplyForce(attackDirection * attack1KnockBackStrength); // Apply knockback

            // Need a method that dosent freeze entire game
            //gameManager.FreezeTime(attack1HitStopDuration);
            playerMovement.FreezeMovement(attack1HitStopDuration);
            enemyMovement.FreezeMovement(attack1HitStopDuration);
        }
    }

    private void Attack1StartRecover()
    {
        // animation 
        // end attack scan
        canAttack1 = false;
        enemiesAlreadyHit.Clear();

        Debug.Log("Attack 1 RECOVER");
    }

    private void Attack1Finish()
    {
        // animation 
        Debug.Log("Attack 1 FINISH");
    }

    private void OnDrawGizmos()
    {
        if (playerMovement == null)
            return;

        // Red circular outline to visulize attack 1
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.DrawWireSphere(
            (playerMovement.lastDirection * ability1Offset) + (Vector2)transform.position,
            attack1Radius);
    }
}
