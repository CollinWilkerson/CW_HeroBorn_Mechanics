using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //triggers when a gameobject enters the sphere
    void OnTriggerEnter(Collider other)
    {
        //checks game object name
        if (other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
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
}
