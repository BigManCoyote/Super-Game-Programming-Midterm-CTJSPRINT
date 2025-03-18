using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalTimeText;

    private bool gameOverTriggered = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TriggerGameOver(float finalTime)
    {
        if (!gameOverTriggered)
        {
            gameOverTriggered = true;
            ShowGameOverScreen(finalTime);
        }
    }

    private void ShowGameOverScreen(float finalTime)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = "💀 GAME OVER 💀";
        }

        if (finalTimeText != null)
        {
            int minutes = Mathf.FloorToInt(finalTime / 60);
            int seconds = Mathf.FloorToInt(finalTime % 60);
            int milliseconds = Mathf.FloorToInt((finalTime * 1000) % 1000);
            finalTimeText.text = $"Final Time: {minutes:00}:{seconds:00}:{milliseconds:000}";
        }

        Time.timeScale = 0f; // Pause the game
        Debug.Log("💀 Game Over! No lives left.");
    }

    public void ResetGameOverState()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Hide Game Over UI
        }
        gameOverTriggered = false;
    }


    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameValues(); // Centralized reset
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}