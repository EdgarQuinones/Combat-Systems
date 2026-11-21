using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : CharacterMovement
{
    public void ApplyForce(Vector2 strength)
    {
        rb.AddForce(strength, ForceMode2D.Impulse);
    }
}
