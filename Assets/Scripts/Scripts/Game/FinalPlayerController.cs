using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine;

public class FinalPlayerController : MonoBehaviour
{
    // Variables for player movement
    public float moveSpeed = 5f; // Speed of player movement
    private CharacterController controller; // Reference to CharacterController component
    private Vector3 moveDirection; // Direction of player movement

    // Variables for shooting and reloading
    public Transform firePoint; // Point where bullets are spawned
    public GameObject bulletPrefab; // Prefab for the bullet object
    public float bulletSpeed = 20f; // Speed of bullets
    public float fireRate = 0.5f; // Rate of fire (bullets per second)
    private float nextFireTime = 0f; // Time of next allowed shot
    public int maxAmmo = 30; // Maximum ammo capacity
    private int currentAmmo; // Current ammo count
    public float reloadTime = 1.5f; // Time taken to reload
    private bool isReloading = false; // Flag for reloading state

    // Variables for melee attack
    public float meleeRange = 2f; // Range of melee attack

    // Variables for sliding
    private bool isSliding = false; // Flag for sliding state

    // Mobile controls
    public VirtualJoystick joystick; // Reference to the virtual joystick

    // Attach this script to the player GameObject
    // Ensure there is only one player GameObject in the scene

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get CharacterController component
        currentAmmo = maxAmmo; // Initialize ammo count
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        // PC controls with WASD
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or left/right arrow keys)
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S or up/down arrow keys)
#else
        // Mobile controls with virtual joystick
        float horizontalInput = joystick.Horizontal; // Get horizontal input from virtual joystick
        float verticalInput = joystick.Vertical; // Get vertical input from virtual joystick
#endif
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized; // Calculate movement direction
        controller.Move(moveDirection * moveSpeed * Time.deltaTime); // Move the player

        // Shooting and reloading
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !isReloading)
        {
            Shoot(); // Fire bullets
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload()); // Start reloading
        }

        // Melee attack
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            MeleeAttack(); // Perform melee attack
        }

        // Sliding
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Slide(); // Perform sliding movement
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + 1f / fireRate; // Calculate time of next allowed shot
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Create bullet object
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); // Get Rigidbody component of bullet
        rb.velocity = firePoint.forward * bulletSpeed; // Set velocity of bullet
        currentAmmo--; // Decrease ammo count
    }

    IEnumerator Reload()
    {
        isReloading = true; // Set reloading flag to true
        yield return new WaitForSeconds(reloadTime); // Wait for reload time
        currentAmmo = maxAmmo; // Refill ammo
        isReloading = false; // Set reloading flag to false
    }

    void MeleeAttack()
    {
        // Perform melee attack within the specified range
    }

    void Slide()
    {
        // Perform sliding movement
    }
}
