using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//added this namespace
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    //gets the parent transform
    public Transform patrolRoute;
    //list to hold transform
    public List<Transform> locations;
    
    private int locationIndex = 0;

    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        // puts all locations in a list
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        //iterates through each child
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        //takes a vector3
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    //triggers when a gameobject enters the sphere
    void OnTriggerEnter(Collider other)
    {
        //checks game object name
        if (other.name == "Player")
        {
            Debug.Log("Player detected - attack!");

            agent.destination = player.position;
        }
    }

    //triggers when gameobject leaves the sphere
    void OnTriggerExit(Collider other)
    {
        //does same thing as before
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }
}
