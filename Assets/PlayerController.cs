using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;                // Movement speed of the player
    [SerializeField] private GameObject gunObject;               // Gun object or bullet prefab
    [SerializeField] private Transform bulletSpawnPoint;         // Point where bullets spawn
    [SerializeField] private float bulletSpeed = 10f;            // Speed of bullets
    [SerializeField] private float fireRate = 0.1f;              // Rate of fire (bullets per second)

    private Rigidbody rb;                                        // Reference to the player's Rigidbody component
    private float nextFireTime;                                  // Time when the next shot can be fired

    void Start()
    {
        rb = GetComponent<Rigidbody>();                         // Get the Rigidbody component of the player
    }

    void Update()
    {
        MovePlayer();                                           // Handle player movement
        HandleShooting();                                       // Handle shooting
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");   // Get horizontal input (A and D keys or left and right arrow keys)
        float verticalInput = Input.GetAxis("Vertical");       // Get vertical input (W and S keys or up and down arrow keys)

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime; // Calculate movement vector
        rb.MovePosition(transform.position + movement);        // Move the player's Rigidbody to the new position
    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) // Check if fire button (left mouse button) is pressed and enough time has passed since last shot
        {
            Shoot();                                            // Shoot if conditions are met
            nextFireTime = Time.time + fireRate;                // Set next fire time
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(gunObject, bulletSpawnPoint.position, bulletSpawnPoint.rotation); // Instantiate bullet object at spawn point
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();    // Get Rigidbody component of bullet
        bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed; // Set velocity of bullet to forward direction * bullet speed
    }
}

   /*
   
   
   
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;
    [SerializeField] private float slideForce = 10f;
    //[SerializeField] private Gun gun;
    //[SerializeField] private Kick kick;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isSliding;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
        if (Input.GetButtonDown("Kick"))
        {
            kick.Execute();
        }
        if (Input.GetButtonDown("Slide"))
        {
            Slide();
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * runSpeedMultiplier : moveSpeed;

        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
    }

    void Slide()
    {
        if (!isSliding)
        {
            rb.AddForce(transform.forward * slideForce, ForceMode.Impulse);
            isSliding = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isSliding = false;
        }
    }
}*/ 



    