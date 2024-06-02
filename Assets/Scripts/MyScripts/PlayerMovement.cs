using System;
using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 movement;
    public float runSpeed;
    
    private float currentMoveSpeed;
    
    public Transform playerViewPoint;
    public float mouseSensitivity;
    private float verticalRotation;
    private Vector2 mouseInput;
    public bool invertLook;
    public Camera playerCamera;
    private Rigidbody rb;

    public InputManager inputManager;

    // Start is called before the first frame update
    void Awake ()
    {
        //characterController = GetComponent<CharacterController>();
        //playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

         if (rb != null)  
         {
            Debug.Log("rigidbody is null for the player");
         }

         //inputManager.actions.Player_PC.ZoomCamera.performed += Context => Zoom(Context.ReadValue<float>());     
         }
    /* void Start()
    {
        //characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
         playerActions = new PlayerActions();

    }*/

    // Update is called once per frame
    void Update()
    {
        
        //mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity; //change it to tkae unity new input system
        mouseInput = inputManager.actions.Player_PC.RotateCamera.ReadValue<Vector2>();


        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z );
        verticalRotation += mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); 

    if (invertLook)
    {
        playerViewPoint.rotation = Quaternion.Euler(-verticalRotation, playerViewPoint.eulerAngles.y, playerViewPoint.eulerAngles.z);
    }




        if (inputManager.actions.Player_PC.Sprint.IsPressed()) //change it to tkae unity new input system
        {
           currentMoveSpeed = runSpeed; 
        }       
        else    
        {
            currentMoveSpeed = moveSpeed;
        }

        movement = inputManager.actions.Player_PC.Move.ReadValue<Vector2>();
        var forward = transform.forward;
        var velocity = (transform.right * movement.x + forward * movement.y).normalized * currentMoveSpeed * 0.1f;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        rb.angularVelocity = Vector3.zero;
    }

    
   //new method 
    private void Zoom(float zoomInput)
    {
        Debug.Log(zoomInput);
    }

    private void LateUpdate()
    {
        playerCamera.transform.position = playerViewPoint.position;
        playerCamera.transform.rotation = playerViewPoint.rotation;
    }

//new methods
    private void OnEnable()
    {
        //inputManager.actions.Player_PC.Enable();
    }
    private void OnDisable()
    {
        //inputManager.actions.Player_PC.Disable();
    }
    
    


}
