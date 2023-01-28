using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dropper : MonoBehaviour
{
    MeshRenderer render;
    Rigidbody body;
    [SerializeField] float timeToWait = 3f;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        body = GetComponent<Rigidbody>();
        
        render.enabled = false;
        body.useGravity = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToWait)
        {
            render.enabled = true;
            body.useGravity = true;
        }                                                          
    }
}
