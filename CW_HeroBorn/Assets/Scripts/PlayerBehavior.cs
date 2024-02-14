using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // easy access modifiers for the movement
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;

    public float jumpVelocity = 5f;

    public float distanceToGround = 0.1f;

    //pulls mask from object
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    //variables to combine speed and direction
    private float vInput;
    private float hInput;

    // initializes rigidbody variable
    private Rigidbody _rb;

    private CapsuleCollider _col;

    //boolean containing the player's jump ability
    private bool canJump = false;
    private bool shoot = false;

    private GameBehavior _gameManager;

    void Start()
    {
        //returns the rigidbody on the player
        _rb = GetComponent<Rigidbody>();

        //returns the capsulecolider
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        //handles whether the player moves forward or backward
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        //handles whether the player turns left or right
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        /*
        Debug.Log("Space: " + Input.GetKey(KeyCode.Space));
        Debug.Log("Grounded: " + Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore));
        Debug.Log("Combo: " + (Input.GetKey(KeyCode.Space) &&
            Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore)));
        */
        //sets the jump for the fixed update
        //.checkcapsule takes the start of the capsule the end of the capsule, the radius to check, the layer to check colisions on, and a function that tells it to ignore triggers
        canJump = (Input.GetKey(KeyCode.Space) && 
            Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore));
        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
        }
       /* 
        // actually moves and rotates the player. Does not use physics engine
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    
    //updates after a set time (0.05 seconds). Used for all physics code apparently.
    void FixedUpdate()
    {
        //the actual jump
        if (canJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            canJump = false;
        }

        //creates a rotational vector. Vector3.up is (0,1,0) as we want to rotate around the y axis. the hInput contains the magnitude and the direction around the y axis we rotate.
        Vector3 rotation = Vector3.up * hInput;

        //A 4D representation of an object and its rotation converted from the Euler vector (don't think about it too hard its just the thing the computer likes).
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime); 

        //Applies force in the direction the player is facing, scaled by vInput and fixedTime.
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        //Applies torque on the object dependent on the qaternion we made
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (shoot)
        {
            //instantiate creates a new bullet based on the object we pass
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;
            //gets the new bullet rigid body
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            //sets the bullet direction to the players heading and gives it a magnitude of the bullet speed
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            shoot = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }

}
