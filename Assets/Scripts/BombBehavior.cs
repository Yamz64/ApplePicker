using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    //The normal behavior of bomb objects

    private Rigidbody2D brb;        //private variable brb (bomb rigid body) of dataType Rigidbody2D
    public Vector2 torqueBounds;    //The lowest and highest possible torque value that can be applied to a bomb, set in the inspector
    public Object explosion;        //The explosion particle effect prefab

    // Start is called before the first frame update
    void Start()
    {
        brb = GetComponent<Rigidbody2D>();  //brb is set equal to the attached Rigidbody2D component
        brb.AddTorque(Random.Range(torqueBounds[0], torqueBounds[1]));  //The bomb is given a set amount of torque that is randomized between the torqueBounds
    }

    //Function called on bomb explosion
    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation); //Instances a bomb explosion particle effect at the bomb position
        Destroy(gameObject);                                            //Destroys the bomb's attached GameObject component
    }
}
