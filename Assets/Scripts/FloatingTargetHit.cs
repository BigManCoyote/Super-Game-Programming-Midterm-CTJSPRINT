using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTargetHit : MonoBehaviour
{
    public float range = 50f; // Shooting distance
    public float damage = 10f; // Damage amount (not used in this example)
    public Camera fpsCamera; // Reference to player's camera

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left-click or assigned fire button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            FloatingTarget target = hit.collider.GetComponent<FloatingTarget>(); // Try to get the script

            if (target != null) // Only call OnHit if the object has the script
            {
                target.OnHit();
            }
        }
    }

}
