using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovment : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 3;
    float jumpPower = 4f;
    public bool issprinting;
    public bool isMoving;
    private float sprintSpeed = 6;
    [SerializeField]private float rotationSpeed = 1000f;
    public GameObject character;
    public Transform model;
    Animator _anim;
    Vector2 movement;
    Vector3 playerMovement;
    PlayerControls controls;
    PlayerInput pi;
    public bool isGamePad, isGrounded;
    void Awake(){
        controls = new PlayerControls();

    }
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        pi = GetComponent<PlayerInput>();
        controls.Player.Fire.performed += _ => Attack();
        controls.Player.Jump.performed += _ => Jump();
    }
    void OnEnable(){
        controls.Enable();

    }
    void OnDisable(){
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        HandleInput();
        playerMovement = new Vector3(movement.x * moveSpeed, 0, movement.y * moveSpeed);
        controller.SimpleMove(playerMovement);

        if(movement.sqrMagnitude > 0.0f){
            isMoving = true;
        Quaternion newrotation = Quaternion.LookRotation(playerMovement,Vector3.up);
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, newrotation, rotationSpeed * Time.deltaTime );
        }else
            isMoving = false;
        
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            issprinting = true;
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            
        }
            else issprinting = false;
        if (issprinting) {
            moveSpeed = sprintSpeed;
            Debug.Log("shift"); 
        }
        else { moveSpeed = sprintSpeed / 2; } 
    }
    void HandleInput(){
        movement = controls.Player.Move.ReadValue<Vector2>();		
        _anim.SetBool("isMoving",isMoving);
    }
    public void OnDeviceChange(PlayerInput pi)
    {
        isGamePad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
        Cursor.visible = !isGamePad;

    }
    public void Attack(){
        _anim.SetTrigger("Attack");
    }
    public void Jump(){
        playerMovement = new Vector3(movement.x, jumpPower, movement.y);
    }
}
