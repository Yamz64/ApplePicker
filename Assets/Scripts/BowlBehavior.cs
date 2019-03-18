using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlBehavior : MonoBehaviour
{
    public float velocity;
    public float horizontalForce;
    public Vector2 horizontalBounds;
    public Vector2 verticalBounds;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2((Random.Range(horizontalBounds[0], horizontalBounds[1]) * horizontalForce), Random.Range(verticalBounds[0], verticalBounds[1]) * velocity));
        rb.AddTorque(Random.Range(-360.0f, 360.0f));
    }
}
