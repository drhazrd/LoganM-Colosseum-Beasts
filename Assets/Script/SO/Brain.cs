using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Brain : ScriptableObject
{
    public abstract void Think(Follower followers);
}