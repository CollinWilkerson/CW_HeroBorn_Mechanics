using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // easy access modifiers for the movement
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;

    //variables to combine speed and direction
    private float vInput;
    private float hInput;

    // initializes rigidbody variable
    private Rigidbody _rb;

    void Start()
    {
        //returns the rigidbody on the player
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //handles whether the player moves forward or backward
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        //handles whether the player turns left or right
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

       /* 
        // actually moves and rotates the player. Does not use physics engine
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    
    //updates after a set time (0.05 seconds). Used for all physics code apparently.
    void FixedUpdate()
    {
        //creates a rotational vector. Vector3.up is (0,1,0) as we want to rotate around the y axis. the hInput contains the magnitude and the direction around the y axis we rotate.
        Vector3 rotation = Vector3.up * hInput;

        //A 4D representation of an object and its rotation converted from the Euler vector (don't think about it too hard its just the thing the computer likes).
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime); 

        //Applies force in the direction the player is facing, scaled by vInput and fixedTime.
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        //Applies torque on the object dependent on the qaternion we made
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
    
}
