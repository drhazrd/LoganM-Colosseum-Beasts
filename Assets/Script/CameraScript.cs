using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;
public class CameraScript: MonoBehaviour{
    [Header("References")]
    Transform orientation;
    Transform player;
    Transform playerObj;
    Rigidbody rb;
    public float rotationSpeed = 7;
    //public Transform combatLookAt;
    //public GameObject thirdPersonCam;
    //public GameObject combatCam;
    //public GameObject topDownCam;

    CinemachineFreeLook virtualCam;
    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    void Start(){
        virtualCam = GetComponent<CinemachineFreeLook>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnEnable(){
        CharacterControls.onSpawned += RegisterPlayer;
    }
    void OnDisable(){
        CharacterControls.onSpawned -= RegisterPlayer;
    }
    void Update(){
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(inputDir!=Vector3.zero){
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
 
    }
    void RegisterPlayer(CharacterControls playerData){
        Transform spawnedPlayer = playerData.transform;
        player = playerData.player;
        playerObj = playerData.playerObj;
        orientation = playerData.orientation;
        rb = playerData.rb;
        virtualCam.Follow = player;
        virtualCam.LookAt = player;
    }
}
