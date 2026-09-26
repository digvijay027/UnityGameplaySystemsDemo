using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}