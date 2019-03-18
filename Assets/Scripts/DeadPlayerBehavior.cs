using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPlayerBehavior : MonoBehaviour
{
    public float velocity;              //float value stores the scaler to the upward force
    public float offset;                //y distance from object center
    public Sprite[] deathposes;         //array stores all of the sprites for the death animation
    public Object bowl;                 //bowl prefab
    private SpriteRenderer deathpose;   //the spriterenderer component attached to the dead player prefab
    private Rigidbody2D rb;             //the Rigidbody2D component attached to the dead player prefab

    //DeathSequence() is the function responsible for all of the visual parts to the death animation
    IEnumerator DeathSequence()
    {
        deathpose.sprite = deathposes[0];       //set the deathpose to the surprised charred state
        rb.gravityScale = 0;                    //set the gravityScale to 0
        yield return new WaitForSeconds(1);     //wait 1 second
        deathpose.sprite = deathposes[1];       //set the deathpose to the normal charred state
        rb.gravityScale = 4;                    //set the gravityScale to 4
        rb.AddForce(transform.up * velocity);   //force the rigidbody up scaled by the velocity variable
    }

    //Death() is the function responsible for loading the scene again
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2.5f);  //wait 2.5 seconds
        SceneManager.LoadScene("Game");         //reload the scene
    }

    //Start() occurs on the first frame
    private void Start()
    {
        Instantiate(bowl, new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), transform.rotation);   //instantiates bowl prefab at every position except y which is added by offset
        deathpose = GetComponent<SpriteRenderer>(); //deathpose is set to the SpriteRenderer component
        rb = GetComponent<Rigidbody2D>();           //rb is set to the Rigidbody2D component attached to 
        StartCoroutine(DeathSequence());            //initialize DeathSequence()
        StartCoroutine(Death());                    //initialize Death()
    }
}
