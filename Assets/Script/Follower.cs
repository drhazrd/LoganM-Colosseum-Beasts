using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Follower : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    public Transform player;
    public Transform target;
    public Brain brain;
    Animator animator;
    public bool grounded;
    public bool canFollow;
    GameObject[] patrolPath;
    int currentPatrolPoint = 0;
    float detectRadius = 10f;
    float searchRadius = 3f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        patrolPath = GameManager.gm.patrolPoints;
        currentPatrolPoint = Random.Range(0, patrolPath.Length);
    }

    void Update()
    {
        grounded = agent.isOnNavMesh;
        player = GameManager.gm.player.transform;
        if(player!=null&&canFollow){
            agent.SetDestination(player.position);
        } else {
            DetectPath();
            NewPathPoint();
        }
        canFollow = PlayerDetected();
        if(animator!=null)AnimationUpdate();
        brain.Think(this);
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
    void AnimationUpdate(){
        if(agent.remainingDistance <= agent.stoppingDistance){
            animator.SetBool("isMoving", false);
        }else{
            animator.SetBool("isMoving", true);
        }
    }
}
