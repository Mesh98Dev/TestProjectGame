using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTest : MonoBehaviour

{
    public Transform playerBody; // Reference to the player's body/character controller
    public float mouseSensitivity = 100f; // Mouse sensitivity for camera movement

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
    }

    void Update()
    {
        // Camera rotation based on mouse movement
        float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Vertical") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation to prevent flipping

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate camera vertically
        playerBody.Rotate(Vector3.up * mouseX); // Rotate player horizontally
    }
}
