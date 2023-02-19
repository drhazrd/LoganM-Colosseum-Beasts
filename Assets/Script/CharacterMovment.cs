using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovment : MonoBehaviour
{
    public static event PlayerSpawned onPlayerSpawned;
    public delegate void PlayerSpawned(CharacterMovment character);
    public float gravityScale = -9.87f;
    public CharacterController controller;
    public float moveSpeed;
    public float jumpPower = 4f;
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
        onPlayerSpawned?.Invoke(this);
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
        HandleMovement();
    }

    void HandleMovement(){
        Vector3 playerMove = new Vector3(movement.x , 0, movement.y);
        controller.Move(playerMove *Time.deltaTime*moveSpeed);
        if(!controller.isGrounded){
            playerMovement.y += (gravityScale * Time.deltaTime); 
        }else if(!controller.isGrounded){


        }
        controller.Move(playerMovement *Time.deltaTime);
        if(movement.sqrMagnitude > 0.0f){
            isMoving = true;
        Quaternion newrotation = Quaternion.LookRotation(playerMove,Vector3.up);
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, newrotation, rotationSpeed * Time.deltaTime );
        }else
            isMoving = false;
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
        Debug.Log("Jump"); 
        controller.Move(new Vector3 (playerMovement.x, jumpPower, playerMovement.z));
    }
}
