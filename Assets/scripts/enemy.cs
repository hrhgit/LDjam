using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public float speed=5f;

    public GameObject enemydie;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direct = player.transform.position - transform.position;
        transform.position += (Vector3)(direct.normalized * speed * Time.deltaTime);
        float distance = Vector2.Distance(player.position, transform.position);
        
        if (distance < gamemanager.instance.mindistance)
        {
            gamemanager.instance.minenemy = transform;
            gamemanager.instance.mindistance = distance;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "bullet")
        {
            Destroy(gameObject);
        }
        if (other.transform.tag == "Player")
        {
            playerControl playerControl = other.transform.GetComponent<playerControl>();
            if (playerControl.mode == playerControl.state.dash)
            {
                Destroy(gameObject);
            }
            
        }
    }

    private void OnDestroy()
    {
        gamemanager.instance.scorecount++;
        gamemanager.instance.mindistance = 100;
        Instantiate(enemydie,transform.position,quaternion.identity);
    }
}