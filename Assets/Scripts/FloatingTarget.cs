using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTarget : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float distance = 5f; // Total distance to travel
    public Vector3 direction = Vector3.right; // Movement direction

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + direction.normalized * pingPong;
    }

    public void OnHit()
    {
        Destroy(gameObject); // Destroys the target
    }
}