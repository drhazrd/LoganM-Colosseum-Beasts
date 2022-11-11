using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    int size;
    Transform target;
    GameObject model;
    void Start()
    {
        model = transform.gameObject;
        size = 1; 
        model.transform.localScale = new Vector3(size,size,size);
    }

    void Update()
    {
        
    }

    void Eat(Transform t){
        GameObject go = t.gameObject;
        Destroy(go);
    }
    void Move(){

    }
    void Grow(){

    }
    void OnCollisionEnter(Collision col){
        if(col.transform.tag!="uneatable")Eat(col.transform);
    }
}
