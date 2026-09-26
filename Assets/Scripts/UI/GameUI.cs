using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private GameObject gameOverPanel;

    private void Update()
    {
        UpdateScore();
        UpdateWave();
        UpdateEnemies();
        UpdateHealth();

        if (GameManager.Instance != null &&
        GameManager.Instance.IsGameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void UpdateScore()
    {
        int score = 0;

        if (GameManager.Instance != null)
        {
            score = GameManager.Instance.Score;
        }

        scoreText.text = "Score: " + score;
    }

    private void UpdateWave()
    {
        if (enemySpawner != null)
        {
            waveText.text = "Wave: " + enemySpawner.CurrentWave;
        }
    }

    private void UpdateEnemies()
    {
        if (enemySpawner != null)
        {
            enemiesText.text = "Enemies: " + enemySpawner.EnemiesAlive;
        }
    }

    private void UpdateHealth()
    {
        if (playerHealth != null)
        {
            healthText.text = "HP: " + playerHealth.CurrentHealth;
        }
    }
}