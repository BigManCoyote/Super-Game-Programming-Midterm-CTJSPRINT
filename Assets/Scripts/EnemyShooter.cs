using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private AudioClip shootSound; // Assign in Unity Inspector
    private AudioSource audioSource;

    public float minShootInterval = 1f;
    public float maxShootInterval = 3f;

    [Range(0f, 1f)] public float shootVolume = 0.4f; // Now set to 0.6

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
        }

        // Force the shootVolume to 0.6
        shootVolume = 0.6f;
        audioSource.volume = shootVolume;

        audioSource.spatialBlend = 1f;  // 3D sound
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.minDistance = 2f;
        audioSource.maxDistance = 20f;

        //Debug.Log("Forced Shoot Volume to: " + shootVolume);
        StartCoroutine(ShootRandomly());
    }

    private IEnumerator ShootRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
            Shoot();
        }
    }

    private void Shoot()
    {
        if (fireballPrefab != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position + transform.forward * 1.5f, transform.rotation);
            Rigidbody rb = fireball.GetComponent<Rigidbody>();

            if (rb == null)
            {
                rb = fireball.AddComponent<Rigidbody>();
            }

            rb.useGravity = false;
            rb.velocity = transform.forward * 10f;

            if (shootSound != null && audioSource != null)
            {
                audioSource.volume = shootVolume; // Force the correct volume before playing
                audioSource.PlayOneShot(shootSound, shootVolume);
                //Debug.Log("Playing Fireball Sound at Volume: " + shootVolume);
            }
        }
    }
}
