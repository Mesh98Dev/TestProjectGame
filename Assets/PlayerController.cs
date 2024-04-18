using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// PlayerController.cs

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
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



    