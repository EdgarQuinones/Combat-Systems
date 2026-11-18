using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyForce(Vector2 strength)
    {
        rb.AddForce(strength, ForceMode2D.Impulse);
    }
}
