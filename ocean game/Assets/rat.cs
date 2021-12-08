using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rat : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    float secondarySpeed;

    float timer;
    float timerGoal;
    public float minTime, maxTime;
    int state, prevState;
    public float idleTime;
    public float magnitude = 10;
    Animator anim;
    AudioSource sor;
    [SerializeField]
    AudioClip[] clips;
    public bool noise = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        sor = GetComponent<AudioSource>();
        setTimerGoal();
        setState();
    }

    void soundPlay()
    {
        if (noise)
        {
            sor.pitch = Random.Range(1.9f, 1.15f);
            sor.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerGoal)
        {
            setState();
            setTimerGoal();
            soundPlay();

        }

        if(state == 1)
        {
            moveRight();
            //Debug.Log("right");
            anim.SetTrigger("horiz");
        }
        else if(state == 2)
        {
            moveLeft();
            //Debug.Log("L");
            anim.SetTrigger("horiz");
        }
        else if (state == 3)
        {
            moveUp();
            //Debug.Log("U");
            anim.SetTrigger("vert");
        }
        else if (state == 4)
        {
            moveDown();
            //Debug.Log("D");
            anim.SetTrigger("vert");
        }
        else if (state >= 5)
        {
            //Debug.Log("I");
            timerGoal = idleTime;
            anim.SetTrigger("idle");
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetTrigger("idle");
            body.velocity = Vector3.zero;
        }

    }


    void setTimerGoal()
    {
        timerGoal = Random.Range(minTime, maxTime);
        timer = 0;
    }
    void setState()
    {
        prevState = state;
        state = Random.Range(1, 9);
        getSecondarySpeed();
        if (state == prevState)
        {
            
            if (Random.Range(1, 10) >= 3)
            {
                setState();
            }
            
           
        }

    }

    void getSecondarySpeed()
    {
        secondarySpeed = Random.Range(-speed / 2f, speed / 2f);
    }

    void moveRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
        body.velocity = new Vector2 (speed * Time.deltaTime, secondarySpeed*Time.deltaTime);
    }
    void moveLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        body.velocity = new Vector2(-speed * Time.deltaTime, secondarySpeed * Time.deltaTime);
    }

    void moveUp()
    {
        transform.localScale = new Vector3(1, 1, 1);
        body.velocity = new Vector2(secondarySpeed * Time.deltaTime, speed * Time.deltaTime);
    }
    void moveDown()
    {
        transform.localScale = new Vector3(1, -1, 1);
        body.velocity = new Vector2(secondarySpeed * Time.deltaTime, -speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int c = Random.Range(1,2);
        if (Random.Range(1, 2) == 2)
        {
            c = -c;
        }
        if(state+c <1 || state + c > 4)
        {
            c = -c;
        }
        state += c;

        // how much the character should be knocked back
        
        // calculate force vector
        var force = transform.position - collision.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        body.velocity = force * magnitude;

    }
}
