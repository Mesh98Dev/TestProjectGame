using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class FpsCamera : MonoBehaviour
{
   
    // Variables for movement speed and rotation speed
//public float moveSpeed = 5f;
//public float rotationSpeed = 5f;
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Reference to the main camera
//private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
//mainCamera = Camera.main;
//Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90f);

        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = quaternion.Euler(0,yRotation,0);
        
        /*
        // Get player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the forward direction of the camera without vertical component
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f; // Zero out the vertical component to keep the movement on the XZ plane

        // Calculate movement direction based on camera's forward vector
        Vector3 movement = (horizontalInput * mainCamera.transform.right + verticalInput * cameraForward).normalized;

        // If there's movement input, rotate player towards the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the player based on the movement direction and speed
        //transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        */

        // Handle camera rotation (for first-person perspective)
        // Get mouse input for rotation
           

        // Rotate the player horizontally based on mouse X movement
//   transform.Rotate(Vector3.up * mouseX * rotationSpeed);

        // Rotate the main camera vertically based on mouse Y movement
        // Note: Clamp the vertical rotation to limit the camera's range of motion
//Vector3 currentRotation = mainCamera.transform.eulerAngles;
//float desiredRotationX = currentRotation.x - mouseY;
//mainCamera.transform.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}
