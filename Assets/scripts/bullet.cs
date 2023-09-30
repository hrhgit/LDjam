using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "wall" || other.transform.tag=="enemy")
        {
            Destroy(gameObject);
        }
    }
}
