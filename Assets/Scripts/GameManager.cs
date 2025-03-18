using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int totalSantas = 4;
    private int collectedSantas = 0;

    [SerializeField] private int totalTargets = 10;
    private int destroyedTargets = 0;

    public TextMeshProUGUI santaCounterText;
    public TextMeshProUGUI targetCounterText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI livesText;

    public AudioSource santaLaughAudio;

    private float gameTime = 0f;
    private bool gameRunning = true;

    public int playerHealth = 2; // ✅ Health starts at 2
    public int playerLives = 3;

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
        StartCoroutine(WaitForUIElements());
        ResetGameValues();
    }

    private void Update()
    {
        if (gameRunning)
        {
            gameTime += Time.deltaTime;
            UpdateTimer();
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        UpdateHealthUI(playerHealth);

        if (playerHealth <= 0)
        {
            LoseLife();
        }
    }

    public float GetGameTime()
    {   
        return gameTime;
    }

    public void LoseLife()
    {
        playerLives--;
        UpdateLivesUI(playerLives);

        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            playerHealth = 2; // ✅ Reset health to 2 on respawn
            UpdateHealthUI(playerHealth);
        }
    }

    public void ResetGameValues()
{
    collectedSantas = 0;
    destroyedTargets = 0;
    gameTime = 0f;
    gameRunning = true;
    playerHealth = 2; // ✅ Reset to 2 at game start
    playerLives = 3;

    StartCoroutine(WaitForUIElements());

    // ✅ Hide Victory UI using existing VictoryManager function
    if (VictoryManager.Instance != null)
    {
        VictoryManager.Instance.ResetVictoryState();
    }

    // ✅ Hide Game Over UI using existing GameOverManager function
    if (GameOverManager.Instance != null)
    {
        GameOverManager.Instance.ResetGameOverState();
    }

}

    public void UpdateTimer()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000);
            timerText.text = $"Time: {minutes:00}:{seconds:00}:{milliseconds:000}";
        }
    }

    public void UpdateSantaCounter()
    {
        if (santaCounterText != null)
        {
            santaCounterText.text = $"Santas: {collectedSantas} / {totalSantas}";
        }
    }

    public void UpdateTargetCounter()
    {
        if (targetCounterText != null)
        {
            targetCounterText.text = $"Targets: {destroyedTargets} / {totalTargets}";
        }
    }

    public void TargetDestroyed()
    {
        destroyedTargets++;
        UpdateTargetCounter();
    }

    public void CollectSanta()
    {
        collectedSantas++;
        UpdateSantaCounter();

        if (collectedSantas >= totalSantas)
        {
            PlayVictorySound();
        }
    }

    private void PlayVictorySound()
    {
        if (santaLaughAudio != null)
        {
            santaLaughAudio.Play();
            Debug.Log("🎅 Santa laughs: Merry Christmas!");
        }
    }

    private void GameOver()
    {
        Debug.Log("💀 Game Over! Restarting...");
        RestartGame();
    }

    public void RestartGame()
    {
        ResetGameValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator WaitForUIElements()
    {
        yield return new WaitForSeconds(0.1f);

        santaCounterText = GameObject.Find("SantaCounterText")?.GetComponent<TextMeshProUGUI>();
        targetCounterText = GameObject.Find("TargetCounterText")?.GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("LivesText")?.GetComponent<TextMeshProUGUI>();

        UpdateSantaCounter();
        UpdateTargetCounter();
        UpdateTimer();
        UpdateHealthUI(playerHealth);
        UpdateLivesUI(playerLives);
    }

    public void UpdateHealthUI(int health)
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }

    public void UpdateLivesUI(int lives)
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {lives}";
        }
    }
}
