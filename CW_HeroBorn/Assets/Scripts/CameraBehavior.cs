using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // distance from player to camera
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f);

    // stores the player object's transform
    private Transform target;

    void Start()
    {
        // creates a cordinate at the center of the player
        target = GameObject.Find("Player").transform; 
    }
    
    void LateUpdate()
    {
        // calculates the position of the player then applies the offset. transforms the camera to that point
        this.transform.position = target.TransformPoint(camOffset);

        // points camera at player
        this.transform.LookAt(target); 
    }
}
