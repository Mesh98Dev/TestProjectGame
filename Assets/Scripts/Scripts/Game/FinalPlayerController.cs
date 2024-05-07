using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f; // Speed of player movement
    public float sprintSpeed = 10f; // Speed of player sprinting
    public float jumpForce = 10f; // Force applied when player jumps
    private bool isGrounded; // Flag to check if player is grounded

    // Shooting variables
    public ParticleSystem gunParticles; // Reference to the gun particles system
    public Transform gunEnd; // Point from where gun particles are emitted
    public float fireRate = 0.5f; // Rate of fire (bullets per second)
    private float nextFireTime = 0f; // Time until next allowed shot

    // Weapon variables
    public GameObject[] weapons; // Array of weapon GameObjects
    private int currentWeaponIndex = 0; // Index of currently equipped weapon

    // Health variables
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

    // Points variables
    public int pointsPerKill = 100; // Points earned per zombie kill
    private int playerPoints = 0; // Total points earned by the player

    // UI variables
    public UIController uiController; // Reference to the UIController script

    public Transform cameraTransform;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum
        uiController.UpdateHealthBar(currentHealth, maxHealth); // Update UI health bar
        uiController.UpdatePoints(playerPoints); // Update UI points display
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

        // Handle weapon switching
        HandleWeaponSwitching();

        // Handle melee attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            MeleeAttack(); // Call the method for melee attack
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input s

        Vector3 moveDirection = cameraTransform.rotation * new Vector3(horizontalInput, 0f, verticalInput).normalized; // Calculate movement direction

        // Move the player based on input
        if (moveDirection != Vector3.zero)
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed; // Apply sprint speed if shift key is pressed
            transform.Translate(moveDirection * speed * Time.deltaTime); // Move the player
        }

        // Handle player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(); // Call the method to make the player jump
        }
    }

    private void Shoot()
    {
        // Play the gun particles at the gunEnd position
        gunParticles.transform.position = gunEnd.position; // Set the gun particles position
        gunParticles.transform.rotation = gunEnd.rotation; // Set the gun particles rotation
        gunParticles.Play(); // Trigger the gun particles
    }

    private void Jump()
    {
        // Apply upward force to the player for jumping
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void HandleWeaponSwitching()
    {
        // Handle scrolling to switch weapons
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Get mouse scroll input
        if (scroll > 0f)
        {
            SwitchWeapon(1); // Switch to the next weapon
        }
        else if (scroll < 0f)
        {
            SwitchWeapon(-1); // Switch to the previous weapon
        }
    }

    private void SwitchWeapon(int direction)
    {
        // Switch to the next or previous weapon based on direction
        currentWeaponIndex += direction; // Update the current weapon index
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1; // Loop back to the last weapon if index goes below 0
        }
        else if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0; // Loop back to the first weapon if index exceeds the length of weapons array
        }

        // Activate the current weapon and deactivate others
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex); // Activate the current weapon and deactivate others
        }
    }

    private void MeleeAttack()
    {
        // Implement melee attack logic
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set the grounded flag to true
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player leaves the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Set the grounded flag to false
        }
    }
}
