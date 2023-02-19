using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour{
    public GameObject[] patrolPoints;
    public BoxCollider collider;

    void Start(){
        patrolPoints = GameObject.FindGameObjectsWithTag("Patrol Point");
        GameManager.gm.patrolPoints = patrolPoints;
        collider.isTrigger = true;
    }

    void OnTriggerExit(Collider col){
        if(col.tag == "Player"){
            Transform reSpawnPos = GameManager.gm.spawnPoint;
            col.transform.position = reSpawnPos.position;
        }
    }
}
