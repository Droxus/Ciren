using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float zoomSpeed = 100.0f;
    public float minZoomDistance = 10.0f;
    public float maxZoomDistance = 200.0f;

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        // Handle zooming
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0.0f)
        {
            float offsetMagnitude = offset.magnitude;
            offsetMagnitude -= scrollInput * zoomSpeed;
            offsetMagnitude = Mathf.Clamp(offsetMagnitude, minZoomDistance, maxZoomDistance);

            offset = offset.normalized * offsetMagnitude;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
