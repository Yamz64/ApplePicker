using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    //The normal behavior attatched to bomb explosion particle effects

    //Death function with returnType IEnumerator
    IEnumerator Death()
    {
        yield return new WaitForSeconds(.4f);   //yields for .4 seconds
        Destroy(gameObject);                    //destroys the attached GameObject component
    }

    //Occurs on the first frame
    private void Start()
    {
        StartCoroutine(Death());                //starts the coroutine - Death
    }
}
