using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    int size;
    Transform target;
    GameObject model;
    public float thrust=.5f;
    public Transform playerPos;
    Rigidbody rb;
    void Start()
    {
        model = transform.gameObject;
        size = 1; 
        model.transform.localScale = new Vector3(size,size,size);
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(target != null) BlackholeDrift();
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
        if(col.transform.tag=="uneatable"||col.transform.tag=="Player")
        { return;} else
        {
            Eat(col.transform);
        }
    }
    void BlackholeDrift(){
        transform.LookAt(playerPos);
        rb.AddForce(transform.forward * thrust);
    }
}
