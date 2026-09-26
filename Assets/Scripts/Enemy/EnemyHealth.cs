using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int scoreValue = 100;

    private int currentHealth;
    private EnemySpawner spawner;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        if (spawner != null)
        {
            spawner.EnemyDefeated();
        }

        Destroy(gameObject);
    }
}