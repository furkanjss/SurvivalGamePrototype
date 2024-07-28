using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float mouseSensitivity = 100f;

    
    [SerializeField] float distanceFromPlayer = 4f;
    [SerializeField] float heightOffset = 2f; 
    [SerializeField] float smoothSpeed = 0.125f; 

    private float xRotation = 0f;
   

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        transform.rotation = Quaternion.Euler(0, xRotation, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position - player.forward * distanceFromPlayer + Vector3.up * heightOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player.position + Vector3.up * heightOffset); 
    }

}
