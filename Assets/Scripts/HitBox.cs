﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBox : MonoBehaviour
{
    //The behavior of the hitbox GameObject, child to the player GameObject
    public int score;                   //score stores the player's points
    public int health;                  //health stores the player's hitpoints
    public int combo;                   //combo stores how many apples are caught consecutively
    public int hiscore;                 //hiscore stores the largest the player's score has ever been
    public float aTimer;                //aTimer (alpha timer) will gradually dim the comboText's alpha value over time
    public float iTimer;                //iTimer (i-frame timer) determines the amount of time the player is in i-frames
    public Text hp;                     //a ui text object that will display the health integer value
    public Text scoreText;              //a ui text object that will display the score integer value
    public Text comboText;              //a ui text object that will display the combo integer value
    public Text hiscoreText;            //a ui text object that will display the hiscore integer value
    public SpriteRenderer playerSprite; //the player's sprite
    public Object deadPlayer;           //a prefab for the dead player object
    private AudioSource[] sounds = new AudioSource[2];       //all audiosources attached to hitbox
    private Animator anim;              //animator attached to player object

    //Initialized on Starting Frame
    private void Start()
    {
        sounds = GetComponents<AudioSource>();          //sets applePick equal to the attached AudioSource component
        anim = GetComponentInParent<Animator>();        //sets anim equal to the player Animator component
        hiscore = PlayerPrefs.GetInt("HiScore");        //hiscore is set to the HiScore Player Preference
        playerSprite = GetComponentInParent<SpriteRenderer>();//playerSprite is set to the SpriteRenderer Component in the parent
    }

    //Function responsible for LifeUP Animation
    private IEnumerator LifeUpAnimation()
    {
        anim.SetBool("LifeUP", true);               //Sets LifeUP bool to true in player animator controller
        yield return new WaitForSeconds(.15f);      //Waits .15 seconds
        anim.SetBool("LifeUP", false);              //Sets LifeUP bool to false in player animator controller
    }

    //Everything that occurs when a Collider2D by the name of other enters the collision box
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Apple")        //if the other collider's tag is called "Apple"...
        {
            combo += 1;                     //combo is incremented by 1
            score += 10 * combo;            //score is updated by 10 * (the currect combo)
            aTimer = 1.0f;                  //aTimer is reset to 1.0f
            sounds[0].Play();               //the apple pick sound is played
            Destroy(other.gameObject);      //the apple is destroyed
        }else if((other.tag == "Bomb") && (iTimer <= 0.0f))   //if the other collider's tag is called "Bomb" and iTimer is less than or equal to 0...
        {
            health -= 1;                                                    //health is decremented by 1
            combo = 0;                                                      //combo is set to 0
            BombBehavior otherBomb = other.GetComponent<BombBehavior>();    //a variable otherBomb of dataType BombBehavior (see BombBehavior script) is set to the attached component BombBehavior of the entering collider
            sounds[1].Play();                                               //the bomb sound is played
            otherBomb.Explode();                                            //otherBomb's Explode method is initialized
            iTimer = 3.0f;                                                  //iframe Timer is set to 3 seconds
        }else if(other.tag == "Feather")//if the other collider's tag is called "Feather"... 
        {
            health += 1;                            //increment health by 1
            StartCoroutine(LifeUpAnimation());      //start LifeUP animation
            sounds[2].Play();                       //the LifeUP sound is played
            Destroy(other.gameObject);              //destroy the feather
        }
    }

    //Occurs on every frame
    private void Update()
    {
        hp.text = "HP: " + health.ToString();               //the text value of hp is set to "HP: " + the health integer as a string (for example: what would be displayed if the health was 2 would be -> HP: 2)
        scoreText.text = "-Score-\n" + score.ToString();    /*the text value of scoreText is set to "-Score-\n" + the score integer as a string (for example: what would be displayed if score was 2500 would be -> -Score-
                                                                                                                                                                                                                     2500   )*/
        hiscoreText.text = "-Hi Score-\n" + hiscore.ToString(); //the text value of hiscoreText is set to "-Hi Score-\n" + the hiscore integer as a string in the same way as the above statement
        if(health == 3)                                     //if the health integer is equal to 3...
        {
            hp.color = new Color(0.0f, 255.0f, 0.0f, 1.0f);     //the color value of hp is set to green
        }else if(health == 2)                               //if the health integer is equal to 2...
        {
            hp.color = new Color(255.0f, 255.0f, 0.0f, 1.0f);   //the color value of hp is set to yellow
        }else if(health == 1)                               //if the health integer is equal to 1...
        {
            hp.color = new Color(255.0f, 0.0f, 0.0f, 1.0f);     //the color value of hp is set to red
        }
        else if(health < 1)                                 //if health is less than 1...
        {
            hp.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);       //the color value of hp is set to black
            PlayerPrefs.SetInt("HiScore", hiscore);             //set the HiScore equal to the integer variable hiscore
            Instantiate(deadPlayer, transform.position, transform.rotation);    //instantiate the dead player object
            GameObject parent = transform.parent.gameObject;    //get the parent of the hitbox
            Destroy(parent);                                    //destroy the parent
        }
        if((combo > 0) && (combo < 20))                     //if the combo integer is greater than 0 and less than 20...
        {
            comboText.text = "Combo!\n" + combo.ToString();     //the comboText text value is set to "Combo!\n" + the combo integer as a string in much the same way as the score text value, but instead of -Score- it's Combo!
        }else if(combo >= 20)                               //if the combo integer is greater than or equal to 20...
        {
            comboText.text = "Wombo Combo!\n" + combo.ToString();   //the comboText text value is set to "Wombo Combo!\n" + the combo integer in the same way as the above if statement
        }
        else                                                //if the combo integer is less than or equal to 0...
        {
            comboText.text = "";                                //the comboText text value is set to "", which is blank
        }
        aTimer -= .5f * Time.deltaTime;                     //aTimer is decremented by half of the change in time
        if (combo < 10)                                                 //if the combo integer is less than 10...
        {
            comboText.color = new Color(255.0f, 255.0f, 255.0f, aTimer);    //the color value of comboText is set equal to white with an alphavalue set to aTimer
        }
        else                                                            //if the combo integer is greather than or equal to 10...
        {
            comboText.color = new Color(255.0f, 255.0f, 0.0f, aTimer);      //the color value of comboText is set equal to yellow with an alphavalue set to aTimer
        }
        if(score > hiscore)     //if score is greater than hiscore...
        {
            hiscore = score;        //set hiscore equal to score
        }
        iTimer -= 1.0f * Time.deltaTime;    //iTimer is decremented over time
        if(iTimer > 0.0f)       //if iTimer is greater than or equal to 0
        {
            playerSprite.color = new Color(255.0f, 255.0f, 255.0f, .5f);    //set alpha value of player's sprite to .5
        }
        else
        {
            playerSprite.color = new Color(255.0f, 255.0f, 255.0f, 1.0f);   //set alpha value of player's sprite to 1
        }
    }
}
