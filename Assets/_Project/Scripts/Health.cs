using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Health : MonoBehaviour
{
    // Components
    SpriteRenderer sr;

    [Header ("Health Options")]
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private Color hurtColor = Color.white;
    [SerializeField] private float hurtFlashtime = 0.1f;
    private Color originalColor;

    private int _currentHealth;
    public int currentHealth => _currentHealth; // read only current health

    private float flashEndTime = 0.0f;
    private bool isFlashing = false;

    private void Awake()
    {
        _currentHealth = startingHealth;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void TakeDamage (int dmg = 10)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - dmg);

        //StartCoroutine(hurtFlash(hurtFlashtime)); // Change the color of the sprite quickly, then go back
        flashEndTime = hurtFlashtime + Time.time;

        // Reset stats incase used later
        if (_currentHealth == 0)
        {
            flashEndTime = 0;
            isFlashing = false;
            sr.color = originalColor;
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Time.time < flashEndTime)
        {
            if (!isFlashing)
            {
                sr.color = hurtColor;
                isFlashing = true;
            }
        } 
        else
        {
            if (isFlashing)
            {
                sr.color = originalColor;
                isFlashing = false;
            }
        }
    }

    //private IEnumerator hurtFlash(float duration)
    //{
    //    totalHits++;
    //    sr.color = hurtColor;

    //    yield return new WaitForSeconds(duration); // Wait before going back to original color

    //    // If enemy takes multiple hits they dont change back to original color
    //    if (totalHits == 1)
    //    {
    //        sr.color = originalColor;
    //    }
    //    totalHits--;
    //}

    // Remaining flash time = 0
    // flash 1 starts now
        // Remaining flash time += 0.1 (0.1)
    // flash 2 starts
        // Remaining flash time += 0.1 (0.2)
    // flash 1 ends 
        // flash time -= 0.1 (0.1)
        // if flashtime remaining == 0
            // DEFAULT COLOR
        // else ignore

}
