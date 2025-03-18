using UnityEngine;
using TMPro; // ✅ Import TextMeshPro namespace

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign in Unity Inspector
    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000); // ✅ Calculate milliseconds

        timerText.text = $"Time: {minutes:00}:{seconds:00}:{milliseconds:000}"; // ✅ Format as MM:SS:MS
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
}
