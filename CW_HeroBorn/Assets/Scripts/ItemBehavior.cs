using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    //holds script
    public GameBehavior gameManager;

    private void Start()
    {
        //combines getting the gameobject and getting its component
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    //Calls on contact collision holds the gameobject that initiated the contact
    void OnCollisionEnter(Collision collision)
    {
        //checks if collider was player
        if(collision.gameObject.name == "Player")
        {
            //Removes anything under the parent of the game object
            Destroy(this.transform.parent.gameObject);

            //confirms pickup
            Debug.Log("Item collected!");

            //increments the variable in GameBehavior
            gameManager.Items += 1;
        }
    }
}
