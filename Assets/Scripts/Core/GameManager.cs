using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score;

    public int Score
    {
        get { return score; }
    }

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore(int amount)
    {
        if (IsGameOver)
        {
            return;
        }

        score += amount;
    }

    public void GameOver()
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;

        Time.timeScale = 0f;

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}