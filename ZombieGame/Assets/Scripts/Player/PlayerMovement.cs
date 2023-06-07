using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region
    public static Transform instance;

    public void Awake()
    {
        instance = this.transform;
    }
    #endregion


    [Header("Movement")]
    //Movement speed variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    [Header("Jump")]
    //Jump variables
    [SerializeField] private float jumpForce;
    private bool hasJumped = false;

    [Header("Gravity")]
    //Gravity varaibles
    [SerializeField] private float gravity;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isPlayerGrounded = false;
    private Vector3 velocity = Vector3.zero;




    private void Start()
    {

        GetReferences();
        moveSpeed = walkSpeed; // Set initial movement speed to walkSpeed
        LockCursor(); //Lock the Cursor at the start
    }

    private void Update()
    {
        CheckIfGrounded(); // Check if player is grounded
        HandleJump(); // Handle player Jump
        ApplyGravity(); // Handle player gravity
        HandleRunning(); // Handle player running
        MovePlayer(); // Handle player movement
        
    }
    //Handles the player movement
    private void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Get Horizontal input
        float moveZ = Input.GetAxisRaw("Vertical"); // Get Vertical input

        moveDirection = new Vector3(moveX, 0, moveZ); // Create movement direction vector
        moveDirection = moveDirection.normalized; // Normalize movement direction
        moveDirection = transform.TransformDirection(moveDirection); // moves where the player is looking
        controller.Move(moveDirection * moveSpeed * Time.deltaTime); // Move the player
    }

    //Handles the player running
    private void HandleRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed; // Increase movement speed t runSpeed when Left Shift is pressed
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed; // Reset movement speed to walkSpeed when Left Shift is released
        }
    }

    //Checks if the player is Grounded
    private void CheckIfGrounded()
    {
        //Checks if the player is on the ground
        isPlayerGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isPlayerGrounded)
        {
            hasJumped = false;
        }
    }

    //Handles the player gravity
    private void ApplyGravity()
    {

        if(isPlayerGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //Reset velocity when player is grounded
        }

        velocity.y += gravity * Time.deltaTime; //Apply gravity to velocity
        controller.Move(velocity * Time.deltaTime); // Move the player based on velocity

    }

    //Handles the player jump
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            //Calculate Jump velocity and apply it when Space is pressed and the player is grounded
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
            hasJumped = true;
            
        }
    }

    //Lock the cursor
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    //Get necessary component references
    private void GetReferences()
    {
        controller = GetComponent<CharacterController>(); //Get reference to the CharacterController component
    }

}
