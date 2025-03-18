using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab; // ✅ Fireball Prefab (Assign in Inspector)
    private Transform player;

    public float speed = 6f;
    public float obstacleRange = 30f;
    public Vector3 boundaryMin;
    public Vector3 boundaryMax;

    private bool isAlive = true;
    private Vector3 moveDirection;
    private float fireballCooldown = 2f; // ✅ Time between shots
    private float lastFireTime = 0f; // ✅ Track last fire time

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform; // ✅ Find the player
        SetInitialDirection();
    }

    private void Update()
    {
        if (!isAlive) return;

        // ✅ Move in the chosen direction
        transform.position += moveDirection * speed * Time.deltaTime;

        // ✅ Randomly rotate over time to prevent straight-line movement
        if (Random.value < 0.02f) // 2% chance every frame
        {
            float smallTurn = Random.Range(-30f, 30f);
            transform.Rotate(0, smallTurn, 0);
            moveDirection = transform.forward;
        }

        // ✅ Reverse direction if outside the boundary
        if (!IsWithinBounds(transform.position))
        {
            ReverseDirection();
        }

        // ✅ Try shooting fireball at player
        TryShootFireball();
    }

    private void TryShootFireball()
    {
        if (player == null) return; // ✅ No player found

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < obstacleRange && Time.time > lastFireTime + fireballCooldown)
        {
            lastFireTime = Time.time; // ✅ Reset cooldown
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        if (fireballPrefab == null)
        {
            Debug.LogError("Fireball prefab is not assigned in Inspector!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player not found! Cannot shoot fireball.");
            return;
        }

        // ✅ Fireball spawns slightly forward and above the enemy
        Vector3 fireballSpawnPos = transform.position + transform.forward * 2.0f + Vector3.up * 1.0f;

        // ✅ Instantiate Fireball
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPos, Quaternion.identity);

        // ✅ Rotate fireball towards the player
        Vector3 directionToPlayer = (player.position - fireballSpawnPos).normalized;
        fireball.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // ✅ Apply velocity to the fireball
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = directionToPlayer * 20f; // ✅ Shoots toward the player
        }

        Debug.Log(gameObject.name + " fired a fireball at the player!");
    }



    private bool IsWithinBounds(Vector3 position)
    {
        return (position.x >= boundaryMin.x && position.x <= boundaryMax.x &&
                position.z >= boundaryMin.z && position.z <= boundaryMax.z);
    }

    private void ReverseDirection()
    {
        float randomAngle = Random.Range(120f, 240f);
        transform.Rotate(0, randomAngle, 0);
        moveDirection = transform.forward;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        if (!isAlive)
        {
            moveDirection = Vector3.zero;
        }
    }

    public void SetInitialDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.Rotate(0, randomAngle, 0);
        moveDirection = transform.forward;
    }
}
