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
    public Transform shootPoint; // Point from where bullets are shot
    public GameObject bulletPrefab; // Prefab of the bullet object
    public float bulletForce = 20f; // Force applied to the bullet
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
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Update next allowed shot time
        }

        // Handle weapon switching
        HandleWeaponSwitching();

        // Handle melee attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            MeleeAttack();
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized; // Calculate movement direction

        // Move the player based on input
        if (moveDirection != Vector3.zero)
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed; // Apply sprint speed if shift key is pressed
            transform.Translate(moveDirection * speed * Time.deltaTime); // Move the player
        }

        // Handle player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Shoot()
    {
        // Instantiate a bullet and apply force to it
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.forward * bulletForce, ForceMode.Impulse);
    }

    private void Jump()
    {
        // Apply upward force to the player for jumping
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void HandleWeaponSwitching()
    {
        // Handle scrolling to switch weapons
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchWeapon(1);
        }
        else if (scroll < 0f)
        {
            SwitchWeapon(-1);
        }
    }

    private void SwitchWeapon(int direction)
    {
        // Switch to the next or previous weapon based on direction
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

    private void MeleeAttack()
    {
        // Implement melee attack logic
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player leaves the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

    // Method to deduct player health when