using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the gameplay HUD and Game Over screen.
/// Polls UI data each frame (lightweight string updates only).
/// Game Over detection is event-driven via GameManager.
/// </summary>
public class GameUI : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private Button restartButton;

    private bool gameOverShown;

    private void Start()
    {
        gameOverShown = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void Update()
    {
        UpdateHUD();
        CheckGameOver();
    }

    private void UpdateHUD()
    {
        if (scoreText != null && GameManager.Instance != null)
            scoreText.text = $"SCORE: {GameManager.Instance.Score:N0}";

        if (waveText != null && enemySpawner != null)
            waveText.text = $"WAVE: {enemySpawner.CurrentWave}";

        if (enemiesText != null && enemySpawner != null)
            enemiesText.text = $"ENEMIES: {enemySpawner.EnemiesAlive}";

        if (healthText != null && playerHealth != null)
            healthText.text = $"HP: {playerHealth.CurrentHealth}";
    }

    private void CheckGameOver()
    {
        if (gameOverShown) return;
        if (GameManager.Instance == null) return;
        if (!GameManager.Instance.IsGameOver) return;

        gameOverShown = true;
        ShowGameOver();
    }

    private void ShowGameOver()
    {
        if (gameOverPanel == null) return;

        if (gameOverScoreText != null && GameManager.Instance != null)
            gameOverScoreText.text = $"Score: {GameManager.Instance.Score:N0}";

        gameOverPanel.SetActive(true);
    }

    private void OnRestartClicked()
    {
        // Hide Game Over panel, then surface the Start panel.
        // The actual scene reload happens when the player presses START.
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        StartScreenUI startScreen = GetComponent<StartScreenUI>();
        //if (startScreen != null)
        //    startScreen.ShowStartPanel();
        GameManager.Instance.RestartGame();
    }

    private void OnDestroy()
    {
        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartClicked);
    }
}