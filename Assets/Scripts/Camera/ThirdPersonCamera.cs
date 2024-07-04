using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;  // The target (player model) the camera will follow
    public Vector3 offset;    // The offset from the target

    void Start()
    {
        // If you don't set the offset in the Inspector, calculate the initial offset
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = target.position + offset;
        
        // Set the camera's position to the desired position
        transform.position = desiredPosition;

        // Optionally, make the camera look at the target
        transform.LookAt(target);
    }
}
