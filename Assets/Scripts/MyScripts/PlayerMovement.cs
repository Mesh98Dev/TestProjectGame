using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController characterController;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirction;
    public Vector3 movement;
    public float runSpeed;
    public float currentMoveSpeed;
    
    public Transform playerViewPoint;
    public float mouseSensitivity;
    private float verticalRotation;
    private Vector2 mouseInput;
    public bool invertLook;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z );
        verticalRotation += mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); 

    if (invertLook)
    {
        playerViewPoint.rotation = Quaternion.Euler(-verticalRotation, playerViewPoint.eulerAngles.y, playerViewPoint.eulerAngles.z);
    }




        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        moveDirction = new Vector3(horizontalInput * moveSpeed,0f, verticalInput * moveSpeed);

        if (Input.GetKey(KeyCode.LeftShift) )
        {
           currentMoveSpeed = runSpeed; 
        }       
        else    
        {
            currentMoveSpeed = runSpeed;
        }

        movement = ((transform.forward * moveDirction.z) + (transform.right * moveDirction.x)) .normalized * currentMoveSpeed;

        characterController.Move(movement * Time.deltaTime);
    }

    private void LateUpdate()
    {
        playerCamera.transform.position = playerViewPoint.position;
        playerCamera.transform.rotation = playerViewPoint.rotation;
    }

    


}
