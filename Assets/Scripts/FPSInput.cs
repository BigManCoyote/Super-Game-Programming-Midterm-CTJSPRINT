using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = -9.8f;
    public float jumpSpeed = 8f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (charController.isGrounded)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;

            moveDirection = new Vector3(deltaX, 0, deltaZ);
            moveDirection = Vector3.ClampMagnitude(moveDirection, speed);
            moveDirection = transform.TransformDirection(moveDirection);

            // ✅ Prevent Jump from opening settings
            if (!SettingsManager.Instance.isSettingsOpen && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y += gravity * Time.deltaTime;
        charController.Move(moveDirection * Time.deltaTime);
    }

}
