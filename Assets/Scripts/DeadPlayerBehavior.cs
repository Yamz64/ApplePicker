using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPlayerBehavior : MonoBehaviour
{
    public float velocity;
    public Sprite[] deathposes;
    private SpriteRenderer deathpose;
    private Rigidbody2D rb;

    IEnumerator DeathSequence()
    {
        deathpose.sprite = deathposes[0];
        rb.gravityScale = 0;
        yield return new WaitForSeconds(1);
        deathpose.sprite = deathposes[1];
        rb.gravityScale = 4;
        rb.AddForce(transform.up * velocity);
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        deathpose = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DeathSequence());
        StartCoroutine(Death());
    }
}
