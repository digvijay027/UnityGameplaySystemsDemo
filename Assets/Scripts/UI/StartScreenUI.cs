using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start screen shown before gameplay begins, and again after a Game Over restart.
/// On first launch: unpauses time and hides the panel.
/// On post-Game-Over restart: reloads the scene via GameManager.
/// </summary>
public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Button startButton;

    private bool gameStarted;

    private void Start()
    {
        Time.timeScale = 0f;
        gameStarted = false;

        if (startPanel != null)
            startPanel.SetActive(true);

        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);
    }

    /// <summary>
    /// Called by GameUI when the player clicks Restart on the Game Over screen.
    /// Re-shows the start panel so the player must press START to actually reload.
    /// </summary>
    public void ShowStartPanel()
    {
        gameStarted = false;
        Time.timeScale = 0f;

        if (startPanel != null)
            startPanel.SetActive(true);
    }

    private void OnStartClicked()
    {

        if (gameStarted) return;
        gameStarted = true;

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            // Game Over restart path — reload the scene for a clean state.
            GameManager.Instance.RestartGame();
        }
        else
        {
            // First launch path — just resume time and hide the panel.
            Time.timeScale = 1f;

            if (startPanel != null)
                startPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (startButton != null)
            startButton.onClick.RemoveListener(OnStartClicked);
    }
}
