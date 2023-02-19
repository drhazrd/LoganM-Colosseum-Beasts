using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public Transform spawnPoint{get; private set;}
    CharacterMovment character;
    public CharacterControls player {get; private set;}
    public GameObject[] patrolPoints;
    public GameObject spawningPlayerCharacter;
    public GameObject spawningNPC;
    public GameObject ui;
    public bool gameStarted {get; private set;}

    void Awake(){
         if (gm != null && GameManager.gm != this)
        {
            Destroy(this);
        }
        else if (GameManager.gm == null)
        {
            gm = this;
        }
    }
    void Start()
    {
        spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        if(spawnPoint!=null){
            Instantiate(spawningPlayerCharacter,spawnPoint.position, spawnPoint.rotation);
            Vector3 offset=new Vector3(-1,0,0);
            Instantiate(spawningNPC,spawnPoint.position+offset, spawnPoint.rotation);
        }
        ui.SetActive(false);
        gameStarted = false;
    }

    void OnEnable(){
        CharacterControls.onSpawned += UpdatePlayer;
        CharacterMovment.onPlayerSpawned += UpdatePlayer;
    }
    void OnDisable(){
        CharacterControls.onSpawned -= UpdatePlayer;
        CharacterMovment.onPlayerSpawned -= UpdatePlayer;
    }
    void ToggleUI()
    {
        if(ui.activeInHierarchy){
            ui.SetActive(false);
        }else{ui.SetActive(true);
        }
    }
    void UpdatePlayer(CharacterMovment newCharacter)
    {
        character = newCharacter;
        ToggleUI();
    }
    void UpdatePlayer(CharacterControls newPlayer)
    {
        player = newPlayer;
        ToggleUI();
        gameStarted = true;
    }
}
