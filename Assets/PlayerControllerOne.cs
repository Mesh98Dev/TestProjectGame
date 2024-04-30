using UnityEngine;

public class PlayerControllerOne : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;                // Movement speed of the player
    [SerializeField] private float lookSpeed = 2f;                // Mouse look sensitivity
    [SerializeField] private Camera playerCamera;                // Reference to the player's camera
    [SerializeField] private GameObject gunObject;               // Gun object or bullet prefab
    [SerializeField] private Transform bulletSpawnPoint;         // Point where bullets spawn
    [SerializeField] private float bulletSpeed = 10f;            // Speed of bullets
    [SerializeField] private float fireRate = 0.1f;              // Rate of fire (bullets per second)

    private Rigidbody rb;                                        // Reference to the player's Rigidbody component
    private float nextFireTime;                                  // Time when the next shot can be fired
    private float verticalLookRotation;                          // Current vertical rotation of the camera

    void Start()
    {
        rb = GetComponent<Rigidbody>();                         // Get the Rigidbody component of the player
        Cursor.lockState = CursorLockMode.Locked;               // Lock the cursor to the center of the screen
        Cursor.visible = false;                                 // Hide the cursor
    }

    void Update()
    {
        MovePlayer();                                           // Handle player movement
        HandleMouseLook();                                      // Handle mouse look
        HandleShooting();                                       // Handle shooting
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");   // Get horizontal input (A and D keys or left and right arrow keys)
        float verticalInput = Input.GetAxis("Vertical");       // Get vertical input (W and S keys or up and down arrow keys)

        // Calculate movement vector relative to the camera's forward direction
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 movement = (horizontalInput * cameraRight + verticalInput * cameraForward).normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);        // Move the player's Rigidbody to the new position
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;    // Get mouse X movement for horizontal rotation
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;    // Get mouse Y movement for vertical rotation

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically and clamp the rotation to prevent flipping
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCamera.transform.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
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
        // Instantiate bullet object at spawn point (the camera's position)
        GameObject bullet = Instantiate(gunObject, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();    // Get Rigidbody component of bullet
        bulletRb.velocity = playerCamera.transform.forward * bulletSpeed; // Set velocity of bullet to camera's forward direction * bullet speed
    }
}
