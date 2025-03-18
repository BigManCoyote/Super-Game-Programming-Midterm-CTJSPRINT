using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Load the first game scene (make sure it's added in Build Settings)
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the game (works in a built version, not in the editor)
        Debug.Log("Game Quit!"); // Just for testing in Unity Editor
    }
}
