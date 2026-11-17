using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    private int _currentHealth;
    public int currentHealth => _currentHealth; // read only current health

    private void Awake()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage (int dmg = 10)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - dmg);

        if (_currentHealth == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
