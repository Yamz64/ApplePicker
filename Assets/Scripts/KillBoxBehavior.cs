using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxBehavior : MonoBehaviour
{
    //The normal behavior of the KillBox the object at the bottom of the screen that destroys missed apples and bombs

    public HitBox player;   //player variable of dataType HitBox (see HitBox script)
    private AudioSource missedApple;    //sound that plays when an apple is missed

    private void Start()
    {
        missedApple = GetComponent<AudioSource>();      //sets missedApple equal to the attached AudioSource    
    }

    //Occurs on the frame a Collider2D named other enters the KillBox trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Apple")        //if the other collider's tag is "Apple"...
        {
            missedApple.Play();
            Destroy(other.gameObject);      //destroy the other collider's attached GameObject
            player.combo = 0;               //reset the player's combo counter
        }else if(other.tag == "Bomb")   //if the other collider's tag is "Bomb"...
        {
            Destroy(other.gameObject);      //destroy the other collider's attached GameObject
        }
    }
}
