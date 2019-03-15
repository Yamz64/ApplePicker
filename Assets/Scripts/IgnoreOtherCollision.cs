using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreOtherCollision : MonoBehaviour
{
    //This script causes the feather object to not interact with bombs and apples after first frame of contact

    //On the first frame of collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        //collides with apple
        if(collision.gameObject.tag == "Apple")
        {
            //ignores collision
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);

        //collies with bomb
        }else if(collision.gameObject.tag == "Bomb")
        {
            //ignores collision
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        }
    }
}
