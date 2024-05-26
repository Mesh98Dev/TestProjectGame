using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f; // Speed of player movement
    public float sprintSpeed = 10f; // Speed of player sprinting
    public float jumpForce = 10f; // Force applied when player jumps
    public float gravity = -9.81f; // Gravity force
    private Vector3 velocity; // Current velocity of the player
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
    private Rigidbody rb; // Rigidbody component

    // Melee attack variables
    public float meleeRange = 2f; // Range of melee attack
    public int meleeDamage = 25; // Damage dealt by melee attack

    private void Start()
    {
        // Initialize health and UI
        currentHealth = maxHealth;
        uiController.UpdateHealthBar(currentHealth, maxHealth);
        uiController.UpdatePoints(playerPoints);

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

        // Handle weapon switching
        HandleWeaponSwitching();

        // Handle melee attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            MeleeAttack(); // Call the method for melee attack
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
        // Play the gun particles at the gunEnd position
        gunParticles.transform.position = gunEnd.position;
        gunParticles.transform.rotation = gunEnd.rotation;
        gunParticles.Play();
    }

    // Handles weapon switching
    private void HandleWeaponSwitching()
    {
        // Handle scrolling to switch weapons
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchWeapon(1); // Switch to the next weapon
        }
        else if (scroll < 0f)
        {
            SwitchWeapon(-1); // Switch to the previous weapon
        }
    }

    // Switches weapons based on direction
    private void SwitchWeapon(int direction)
    {
        currentWeaponIndex += direction;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        else if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        // Activate the current weapon and deactivate others
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    // Handles melee attack
    private void MeleeAttack()
    {
        // Perform a sphere cast around the player to detect nearby enemies
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeRange);

        // Loop through all colliders detected in the sphere cast
        foreach (Collider col in hitColliders)
        {
            // Check if the collider belongs to an enemy
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Play melee attack animation
                // Your code to trigger the melee attack animation goes here

                // Play melee attack sound
                // Your code to play the sound effect for the melee attack goes here

                // Deal damage to the enemy
                //enemyHealth.TakeDamage(meleeDamage);
            }
        }
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
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Handle player death
        }
        uiController.UpdateHealthBar(currentHealth, maxHealth);
    }

    // Method to add points
    public void AddPoints(int amount)
    {
        playerPoints += amount;
        uiController.UpdatePoints(playerPoints);
    }
}




