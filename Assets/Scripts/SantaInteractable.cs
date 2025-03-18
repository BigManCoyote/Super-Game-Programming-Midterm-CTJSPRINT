using UnityEngine;

public class SantaInteractable : MonoBehaviour
{
    private bool isCollected = false;
    private bool playerNearby = false; // ✅ Track if player is close

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true; // ✅ Player is near Santa
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false; // ✅ Player left Santa's range
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !isCollected)
        {
            CollectSanta();
        }
    }

    private void CollectSanta()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectSanta();
        }
        else
        {
            Debug.LogError("🚨 GameManager is missing! Santa counter won't update.");
        }

        gameObject.SetActive(false); // ✅ Hide Santa after collection
    }

}
