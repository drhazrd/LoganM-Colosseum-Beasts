using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControls : MonoBehaviour
{
    public static event Spawned onSpawned;
    public delegate void Spawned(CharacterControls player);
    
    [Header("Movement")]
    //public bool canMove;
    public float moveSpeed;
    private Vector2 move;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    bool grounded;
    
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb {get; private set;}
    
    PlayerControls playerControls;
    PlayerInput playerInput;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    void Awake(){
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }
    void Start(){
        onSpawned?.Invoke(this);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void OnEnable(){
        playerControls.Enable();
    }
    void OnDisable(){
        playerControls.Disable();
    }
    void Update(){
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        MyInput();
        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0; 
        }
    }
    void FixedUpdate(){
        MovePlayer();
    }
    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        move = playerControls.Player.Move.ReadValue<Vector2>();
        if(playerControls == null)if(Input.GetKey("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }else{

        }
    }
    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 11f, ForceMode.Force);
    }
     private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}