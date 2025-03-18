using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public int maxLives = 3;
    public int maxHealthPerLife = 100;
    private int currentHealth;
    private int currentLives;

    private Transform lastCheckpoint;
    private bool reachedEndGameCheckpoint = false;

    void Start()
    {
        currentLives = maxLives;
        currentHealth = maxHealthPerLife;
        lastCheckpoint = transform;

        UpdateUI();
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            LoseLife();
        }
        else
        {
            UpdateUI();
        }
    }

    private void LoseLife()
    {
        currentLives--;

        if (currentLives > 0)
        {
            Debug.Log($"Lost a life! Lives left: {currentLives}");
            Respawn();
        }
        else
        {
            GameOver();
        }

        UpdateUI();
    }

    private void Respawn()
    {
        Debug.Log("Respawning...");

        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        transform.position = lastCheckpoint.position;

        if (cc != null) cc.enabled = true;

        currentHealth = maxHealthPerLife;
        UpdateUI();
    }

    private void GameOver()
{
    Debug.Log("💀 Game Over! No lives left.");

    if (GameOverManager.Instance != null && GameManager.Instance != null)
    {
        GameOverManager.Instance.TriggerGameOver(GameManager.Instance.GetGameTime());
    }
    else
    {
        Debug.LogError("🚨 GameOverManager instance is missing! Restarting scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


    public void SetCheckpoint(Transform checkpointTransform, bool isEndGame)
{
    if (!reachedEndGameCheckpoint)
    {
        lastCheckpoint = checkpointTransform;
        Debug.Log($"Checkpoint updated: {lastCheckpoint.position}");

        if (isEndGame)
        {
            reachedEndGameCheckpoint = true;
            Debug.Log("🏆 End Game Checkpoint Reached! YOU WIN!");

            if (VictoryManager.Instance != null && GameManager.Instance != null)
            {
                VictoryManager.Instance.TriggerVictory(GameManager.Instance.GetGameTime());
            }
        }
    }
}

    private void UpdateUI()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealthUI(currentHealth);
            GameManager.Instance.UpdateLivesUI(currentLives);
        }
        else
        {
            Debug.LogError("🚨 GameManager instance is missing! UI won't update.");
        }
    }
}
