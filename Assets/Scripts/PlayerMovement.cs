using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //The behavior that governs player movement

    public Vector3 moveChange;  //A Vector3 that stores the change in direction
    public float speed;         //A movement multiplier
    private CharacterController charControl;    //a variable charControl of dataType CharacterController

    // Start is called before the first frame update
    void Start()
    {
        charControl = GetComponent<CharacterController>();  //charControl is set equal to the attached CharacterController component
    }

    // Update is called once per frame
    void Update()
    {
        moveChange = new Vector3(speed * Input.GetAxis("Horizontal"), 0.0f, 0.0f);  //moveChange's x value is set equal to the "Horizontal" Input Axis who's default inputs are A,D,<-, and ->
        moveChange *= speed;    //all values in moveChange are scaled by speed

        charControl.Move(moveChange * Time.deltaTime);  //charControl is moved by moveChange and smoothed by the change in time
    }
}
