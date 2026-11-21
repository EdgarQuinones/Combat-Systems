using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))] // Rigibody2D is needed for movement to work
public class CharacterMovement : MonoBehaviour
{
    protected Rigidbody2D rb;

    private bool _isFrozen = false;
    public bool isFrozen => _isFrozen;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FreezeMovement(float duration)
    {
        if (isFrozen)
            return;

        StartCoroutine(_FreezeMovement(duration));
    }

    private IEnumerator _FreezeMovement(float duration)
    {
        _isFrozen = true;

        // save current movement 
        Vector2 savedVelocity = rb.linearVelocity;

        // freeeze player 
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(duration);

        // unfreeze player
        // set previous velocity
        rb.linearVelocity = savedVelocity;

        _isFrozen = false;
    }
}
