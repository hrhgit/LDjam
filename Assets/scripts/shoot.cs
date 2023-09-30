using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public Camera m_camera;
    public Transform gunPivot;
    public float rotationSpeed=10f;
    public GameObject bullet;
    public Transform firePoint;
    private bool cooldown=true;
    public float fireForce=500f;
    public Transform player;
    public float fireInterval = 0.1f;
    Vector2 targetPos=Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemanager.instance.minenemy != null)
        {
            Transform e = gamemanager.instance.minenemy;
            targetPos = e.position;
            RotateGun(targetPos);
            if (e.position.x < 5f && e.position.x > -5f && e.position.y < 5f && e.position.y > -5f)
                if (cooldown)
                {
                    GameObject bb = Instantiate(bullet, firePoint.position, Quaternion.identity);
                    Rigidbody2D rb = bb.GetComponent<Rigidbody2D>();
                    rb.velocity = player.GetComponent<Rigidbody2D>().velocity;
                    rb.AddForce(fireForce * (firePoint.position - gunPivot.position).normalized);
                    StartCoroutine(timer(fireInterval));
                }
        }
    }
    void RotateGun(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        
    }
    IEnumerator timer(float time)
    {
        cooldown = false;
        for (float i = time; i >= 0; i -= Time.deltaTime)
        {
            yield return 0;
        }

        cooldown = true;
    }
}