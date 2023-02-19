using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "AI Brain/Follower")]
public class FollowerBrain : Brain
{
    public string targetTag = "Player";
    public override void Think(Follower follower){
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if(target){
            follower.player = target.transform; 
        }
    }
}