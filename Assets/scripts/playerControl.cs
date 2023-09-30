using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class playerControl : MonoBehaviour
{
    public enum state
    {
        move,
        dash,
    }

    [HideInInspector]public state mode = state.move;
    public float speed = 5f;
    private Rigidbody2D _rigidbody2D;
    float horizontal; 
    float vertical;
    private bool cooldown=true;
    public float dashInterval = 1f;
    public float dashDistance = 5f;
    public float dashTime = 0.2f;
    private TrailRenderer trail;
    public AnimationCurve curve;
    public Transform firePoint;

    public GameObject die;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale=Vector3.one*(1+0.1f*gamemanager.instance.scorecount);
        horizontal=Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Mouse1) && cooldown)
        {
            mode = state.dash;
            StartCoroutine(dash());
            StartCoroutine(timerlock(dashInterval));
        }
            
    }

    private void FixedUpdate()
    {
        if (mode == state.move)
        {
            Vector2 move = new Vector2(horizontal * speed, vertical * speed * 0.8f);
            _rigidbody2D.MovePosition(_rigidbody2D.position + move * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "enemy" && mode!=state.dash)
        {
            die.SetActive(true);
            Time.timeScale = 0;
        }
    }
    IEnumerator timerlock(float time)
    {
        cooldown = false;
        for (float i = time; i >= 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        cooldown = true;
    }

    IEnumerator dash()
    {
        trail.enabled = true;
        trail.startWidth = 1 + 0.1f * gamemanager.instance.scorecount;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 stPos = transform.position;
        Vector2 endPos =(Vector2) transform.position + (mousePos - stPos).normalized * dashDistance;
        for (float i = dashTime; i >= 0; i -= Time.deltaTime)
        {
            _rigidbody2D.MovePosition( Vector2.Lerp(stPos,endPos, curve.Evaluate(1-i / dashTime)));
            yield return new WaitForFixedUpdate();
        }
        //transform.position = endPos;
        mode = state.move;
        trail.enabled = false;
    }
}