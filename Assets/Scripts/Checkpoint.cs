using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isEndGameCheckpoint = false; // ✅ Toggle this in the Unity Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player is tagged correctly
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                player.SetCheckpoint(transform, isEndGameCheckpoint);
                Debug.Log($"Checkpoint set at {transform.position}, End Game: {isEndGameCheckpoint}");
            }
        }
    }
}