using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //destroys the bullet after float seconds
        Destroy(this.gameObject, onscreenDelay);
    }
}
