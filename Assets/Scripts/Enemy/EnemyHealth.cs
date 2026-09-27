using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int scoreValue = 100;

    /// <summary>Fired when this enemy takes a non-lethal hit.</summary>
    public event Action OnHit;

    /// <summary>Fired just before this enemy is destroyed.</summary>
    public event Action OnDeath;

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
        else
        {
            OnHit?.Invoke();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();

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