using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HitBox : MonoBehaviour
{
    //The behavior of the hitbox GameObject, child to the player GameObject
    public int score;                   //score stores the player's points
    public int health;                  //health stores the player's hitpoints
    public int combo;                   //combo stores how many apples are caught consecutively
    public float aTimer;                //aTimer (alpha timer) will gradually dim the comboText's alpha value over time
    public Text hp;                     //a ui text object that will display the health integer value
    public Text scoreText;              //a ui text object that will display the score integer value
    public Text comboText;              //a ui text object that will display the combo integer value
    private AudioSource[] sounds = new AudioSource[2];       //all audiosources attached to hitbox

    //Initialized on Starting Frame
    private void Start()
    {
        sounds = GetComponents<AudioSource>();    //sets applePick equal to the attached AudioSource component
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
        }else if(other.tag == "Bomb")   //if the other collider's tag is called "Bomb"...
        {
            health -= 1;                                                    //health is decremented by 1
            combo = 0;                                                      //combo is set to 0
            BombBehavior otherBomb = other.GetComponent<BombBehavior>();    //a variable otherBomb of dataType BombBehavior (see BombBehavior script) is set to the attached component BombBehavior of the entering collider
            sounds[1].Play();                                               //the bomb sound is played
            otherBomb.Explode();                                            //otherBomb's Explode method is initialized
        }
    }

    //Occurs on every frame
    private void Update()
    {
        hp.text = "HP: " + health.ToString();               //the text value of hp is set to "HP: " + the health integer as a string (for example: what would be displayed if the health was 2 would be -> HP: 2)
        scoreText.text = "-Score-\n" + score.ToString();    /*the text value of scoreText is set to "-Score-\n" + the score integer as a string (for example: what would be displayed if score was 2500 would be -> -Score-
                                                                                                                                                                                                                     2500   )*/
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
        else                                                //if none of the above if statements are true...
        {
            hp.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);       //the color value of hp is set to black
            SceneManager.LoadScene("Game");
        }
        if(combo > 0)                                       //if the combo integer is greater than 0...
        {
            comboText.text = "Combo!\n" + combo.ToString();     //the comboText text value is set to "Combo!\n" + the combo integer as a string in much the same way as the score text value, but instead of -Score- it's Combo!
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
    }
}
