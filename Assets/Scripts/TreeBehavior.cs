using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    public float sadTimer;          //float value that determines how long the tree is in the sad state
    public float angerTimer;        //float value that determines how long the tree is in the angry state
    public float nextBombTimer;     //float value that determines time between bombs
    public float nextAppleTimer;    //float value that determines time between apples
    public int featherChance;       //int value that determines chance to drop feather
    public bool sad;                //bool that determines whether the tree is sad or not
    public Vector2 sadBounds;       //the lowest and highest possible amounts of time the tree can be sad
    public Vector2 bombBounds;      //the lowest and highest possible amounts of time that a bomb can spawn
    public Vector2 appleBounds;     //the lowest and highest possible amounts of time that an apple can spawn
    public Animator anim;           //the animator controller attached to the object
    public Object[] dropables;      //array of dropable prefabs
    public Transform leaves;        //the anchor transform for instantiation

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    //sets anim equal to the attached animator component
        sad = true;                         //tree starts sad
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Angry", !sad);        //sets "Angry" parameter in the anim component to what sad is not
        if (sad) {                                  //if the tree is sad...
            sadTimer -= 1 * Time.deltaTime;             //decrement sadTimer over time
            nextAppleTimer -= 1 * Time.deltaTime;       //decrement nextAppleTimer over time
            if (nextAppleTimer <= 0)                    //if nextAppleTimer is less than or equal to 0...
            {
                featherChance = Random.Range(0, 26);
                if(featherChance != 25)
                {
                    Instantiate(dropables[1], new Vector3(leaves.position.x + Random.Range(-5f, 5f), leaves.position.y + Random.Range(-2f, 2f), leaves.position.z), leaves.rotation);   //spawn apple at randomized location from leaves anchor
                    nextAppleTimer = Random.Range(appleBounds[0], appleBounds[1]);      //reset the nextAppleTimer to a random value between the appleBounds
                }
                else
                {
                    Instantiate(dropables[2], new Vector3(leaves.position.x + Random.Range(-5f, 5f), leaves.position.y + Random.Range(-2f, 2f), leaves.position.z), leaves.rotation);   //spawn feather at randomized location from leaves anchor
                    nextAppleTimer = Random.Range(appleBounds[0], appleBounds[1]);      //reset the nextAppleTimer to a random value between the appleBounds
                }
            }
                
            if (sadTimer <= 0.0f)                                   //if sadTimer is less than or equal to 0...
            {
                sad = false;                                            //make the tree angry
                sadTimer = Random.Range(sadBounds[0], sadBounds[1]);    //reset the sad timer to a random value between the sadBounds
            }
        }
        else                                                        //if tree is angry...
        {
            angerTimer -= 1 * Time.deltaTime;                           //decrement anger timer over time
            nextBombTimer -= 1 * Time.deltaTime;                        //decrement the next bomb timer over time
            if(nextBombTimer <= 0.0f)                                   //if nextBombTimer is less than or equal to 0...
            {
                featherChance = Random.Range(0, 51);
                if(featherChance != 50)
                {
                    Instantiate(dropables[0], new Vector3(leaves.position.x + Random.Range(-5f, 5f), leaves.position.y + Random.Range(-2f, 2f), leaves.position.z), leaves.rotation);   //spawn bomb at randomized location from leaves anchor
                    nextBombTimer = Random.Range(bombBounds[0], bombBounds[1]);     //reset the nextBombTimer to a random value between the bombBounds
                }
                else
                {
                    Instantiate(dropables[2], new Vector3(leaves.position.x + Random.Range(-5f, 5f), leaves.position.y + Random.Range(-2f, 2f), leaves.position.z), leaves.rotation);   //spawn feather at randomized location from leaves anchor
                    nextBombTimer = Random.Range(bombBounds[0], bombBounds[1]);     //reset the nextBombTimer to a random value between the bombBounds
                }
            }
            if(angerTimer <= 0.0f)  //if angerTimer is less than or equal to 0...
            {
                sad = true;             //make the tree sad
                angerTimer = 3.0f;      //reset the angerTimer to 3 seconds
            }
        }
    }
}
