using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The default apple behavior script
public class AppleBehavior : MonoBehaviour
{
    private Rigidbody2D brb;                //The physics component attached to the apple object
    public Vector2 torqueBounds;            //The lowest and highest possible torque value that can be applied to an apple, set in the inspector
    // Start is called before the first frame update
    void Start()
    {
        brb = GetComponent<Rigidbody2D>();  //brb is set equal to the Rigidbody2D component attached to the apple
        brb.AddTorque(Random.Range(torqueBounds[0], torqueBounds[1]));  //The apple is given a set amount of torque that is randomized between the torqueBounds
    }
}
