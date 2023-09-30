using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public float speed=5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direct = player.transform.position - transform.position;
        transform.position += (Vector3)(direct * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
