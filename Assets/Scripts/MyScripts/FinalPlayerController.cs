using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlayerController : MonoBehaviour
{

    // Movement variables
    public float moveSpeed = 5f; // Speed of player movement
    public float sprintSpeed = 10f; // Speed of player sprinting
    public float jumpForce = 10f; // Force applied when player jumps
    public float gravity = -9.81f; // Gravity force
    private Vector3 velocity; // Current velocity of the player
    private bool isGrounded; // Flag to check if player is grounded

    // Shooting variables
    public float fireRate = 0.5f; // Rate of fire (bullets per second)
    private float nextFireTime = 0f; // Time until next allowed shot

    // UI variables
    // public UIController uiController; // Reference to the UIController script

    public Transform cameraTransform;
    private Rigidbody rb; // Rigidbody component

    private void Start()
    {
        // Initialize health and UI
        // currentHealth = maxHealth;
        // uiController.UpdateHealthBar(currentHealth, maxHealth);

        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Handle player movement
        HandleMovement();

        // Handle shooting
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot(); // Call the method to shoot
            nextFireTime = Time.time + 1f / fireRate; // Update next allowed shot time
        }
    }

    // Handles player movement
    private void HandleMovement()
    {
        // Get input for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction relative to camera
        Vector3 moveDirection = (cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput).normalized;
        moveDirection.y = 0f; // Ensure the player doesn't move vertically

        // Calculate movement speed based on sprinting
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        Vector3 moveVelocity = moveDirection * speed;

        // Apply movement to Rigidbody
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

        // Handle player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Handles shooting
    private void Shoot()
    {
        // Implement shooting logic here
        Debug.Log("Player shoots");
    }

    // Check if the player is grounded
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Check if the player touches the ground
        }
    }

    // Check if the player leaves the ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Check if the player leaves the ground
        }
    }

    // Method to take damage
    // public void TakeDamage(int amount)
    // {
    //     currentHealth -= amount;
    //     if (currentHealth <= 0)
    //     {
    //         currentHealth = 0;
    //         Die(); // Handle player death
    //     }
    //     uiController.UpdateHealthBar(currentHealth, maxHealth);
    // }

    // Method to handle player death
    // private void Die()
    // {
    //     // Play death animation
    //     // Your code to trigger the death animation goes here

    //     // End the game or respawn the player
    //     // Your code to handle the end of the game or respawn logic goes here

    //     Debug.Log("Player has died.");
    // }
}





