using UnityEngine;
using System.Collections;

public class PosterScript : MonoBehaviour
{    public float delay = 3f;

    // Move to the target end position.
    void Update()
    {
        StartCoroutine(WaitAndDestroy());


    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}