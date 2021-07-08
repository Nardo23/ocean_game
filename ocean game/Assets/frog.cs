using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour
{
    [SerializeField]
    GameObject[] targets;
    GameObject CurrentTarget;
    GameObject previousTarget;
    Animator anim;
    AudioSource sor;
    [SerializeField]
    AudioClip[] clips;
    public Vector2 PitchRange;

    float timer = 0f;
    float jumpTime;
    bool jumping;
    public float speed;
    bool croaked = false;


    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        SetJumpTime();
        CurrentTarget = targets[0];
        transform.position = targets[0].transform.position;
        SetTarget();
    }


    void SetJumpTime()
    {
        jumpTime = Random.Range(.1f, 7f);
    }
    void SetTarget()
    {
        previousTarget = CurrentTarget;
        CurrentTarget = targets[Random.Range(0, targets.Length)];
        if(CurrentTarget == previousTarget)
        {
            SetTarget();
        }


    }

    void jump()
    {
        transform.position = Vector2.MoveTowards(transform.position, CurrentTarget.transform.position, speed* Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Debug.Log("time: " + timer + "/" + jumpTime);

        if(jumpTime <= timer)
        {
            if (!croaked)
            {
                sor.pitch = Random.Range(PitchRange.x, PitchRange.y);
                sor.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            }
            

            croaked = true;
            jumping = true;
            if(CurrentTarget.transform.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            anim.SetBool("jumping", jumping);
            
        }
        else
        {
            transform.position = previousTarget.transform.position;
        }
        if (jumping)
        {
            jump();
            
            if (Vector3.Distance(transform.position, CurrentTarget.transform.position) <.2f)
            {
                
                transform.position = CurrentTarget.transform.position;
                jumping = false;
                croaked = false;
                anim.SetBool("jumping", jumping);
                timer = 0;
                SetJumpTime();
                SetTarget();
            }

        }



    }
}
