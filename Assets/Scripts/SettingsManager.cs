using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public GameObject settingsPanel;
    public bool isSettingsOpen = false;

    public Slider speedSlider;
    public Slider jumpSlider;

    private FPSInput playerController;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            isSettingsOpen = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<FPSInput>();
        }

        if (speedSlider != null)
        {
            speedSlider.onValueChanged.AddListener(UpdateSpeed);
        }
        if (jumpSlider != null)
        {
            jumpSlider.onValueChanged.AddListener(UpdateJumpSpeed);
        }
    }

    // ✅ This function should ONLY be called by the UI button!
    public void ToggleSettings()
    {
        if (settingsPanel == null) return;

        isSettingsOpen = !settingsPanel.activeSelf;
        settingsPanel.SetActive(isSettingsOpen);

        // ✅ Keep cursor unlocked when settings are open
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ✅ Remove button focus to prevent Spacebar issues
        EventSystem.current.SetSelectedGameObject(null);

        Debug.Log("Settings " + (isSettingsOpen ? "Opened - Cursor Unlocked" : "Closed - Cursor Still Unlocked"));
    }

    public void UpdateSpeed(float value)
    {
        if (playerController != null)
        {
            playerController.speed = value;
        }
    }

    public void UpdateJumpSpeed(float value)
    {
        if (playerController != null)
        {
            playerController.jumpSpeed = value;
        }
    }
}
