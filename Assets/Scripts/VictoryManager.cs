using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI finalTimeText;

    private bool gameWon = false;

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
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    public void TriggerVictory(float finalTime)
    {
        if (!gameWon)
        {
            gameWon = true;
            ShowVictoryScreen(finalTime);
        }
    }

    private void ShowVictoryScreen(float finalTime)
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (finalTimeText != null)
        {
            int minutes = Mathf.FloorToInt(finalTime / 60);
            int seconds = Mathf.FloorToInt(finalTime % 60);
            int milliseconds = Mathf.FloorToInt((finalTime * 1000) % 1000);
            finalTimeText.text = $"Final Time: {minutes:00}:{seconds:00}:{milliseconds:000}";
        }

        Time.timeScale = 0f; // Pause the game
        Debug.Log("Victory! Game Over.");
    }

    public void ResetVictoryState()
{
    if (victoryPanel != null)
    {
        victoryPanel.SetActive(false); // Hide Victory UI
    }
    gameWon = false; // Reset the victory flag
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
