using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;
public class CameraController : MonoBehaviour {
    

    private Vector3 offset;
    public GameObject player;
    private Vector3 offsetPlayer = new Vector3(0,5,1);

    void Start ()
    {
        
        transform.position = player.transform.position ;
        offset = transform.position - player.transform.position + offsetPlayer;
    }
    
    
    void Update ()
    {
        
    }
    
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }
    
}