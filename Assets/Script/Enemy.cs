using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState {Searching, Attacking, Idling};
public enum AITeam {Ally, Enemy, Defensive, Aggressive};

public class Enemy : MonoBehaviour
{
    public AIState currentState = AIState.Searching;
    [SerializeField] NavMeshAgent agent;
    public Transform player;
    public bool grounded;
    public bool canFollow;
    GameObject[] patrolPath;
    int currentPatrolPoint = 0;
    float detectRadius = 10f;
    float searchRadius = 3f;
    float timeAttacking;
    float maxTimeAttacking;
    float timeIdling;
    float maxTimeIdling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPath = GameManager.gm.patrolPoints;
        currentPatrolPoint = Random.Range(0, patrolPath.Length);
    }

    void Update()
    {
        grounded = agent.isOnNavMesh;
        player = GameManager.gm.player.transform;
        StateUpdate();
        if(player!=null&&canFollow){
        }
        canFollow = PlayerDetected();
    }
    void DetectPath(){
        if(Vector3.Distance(transform.position, patrolPath[currentPatrolPoint].transform.position) < searchRadius){
            currentPatrolPoint = Random.Range(0, patrolPath.Length);
        }
    }
    void NewPathPoint(){
        agent.SetDestination(patrolPath[currentPatrolPoint].transform.position);
    }
    bool PlayerDetected(){
        if(Vector3.Distance(transform.position, player.transform.position)<detectRadius){
            return true;
        }else{
            return false;
        }
    }
    void StateUpdate() {
        switch (currentState) {
            case AIState.Searching:
                NewPathPoint();
                Debug.Log("Search");
                agent.isStopped = false;
                if (Vector3.Distance(transform.position, player.transform.position) < detectRadius) {
                    currentState = AIState.Attacking;
                } else DetectPath();

                break;
            case AIState.Attacking:
                NewPathPoint();
                Debug.Log("Attack");
                agent.isStopped = true;
                if (Vector3.Distance(transform.position, player.transform.position) > searchRadius) {
                    currentState = AIState.Searching;
                }else if(timeAttacking > maxTimeAttacking){
                    timeAttacking = 0;
                    currentState = AIState.Idling;
                }
                timeAttacking += Time.deltaTime;
                break;
            case AIState.Idling:
                NewPathPoint();
                Debug.Log("Idle");
                agent.isStopped = true;
                if(timeIdling > maxTimeIdling){
                    timeIdling = 0;
                    currentState = AIState.Searching;
                }
                timeIdling += Time.deltaTime;
                break;
        }
    }
}
