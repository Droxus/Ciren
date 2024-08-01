using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float sprintMultiplier = 1.8f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move() 
    {
        vertVelControl();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        
        movement = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0) * movement;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;

        controller.Move(movement * Time.deltaTime * currentSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Rotate()
    {
        Vector3 mousePosition = Input.mousePosition;

        int centerX = Screen.width / 2;
        int centerY = Screen.height / 2;

        int diffX = (int)mousePosition.x - centerX;
        int diffY = (int)mousePosition.y - centerY;

        float angle = -Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    void vertVelControl()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }
}

