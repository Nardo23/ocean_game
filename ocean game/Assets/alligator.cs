using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alligator : MonoBehaviour
{
    animalLand landScript;
    public float landSpeed = 8;
    public float waterSpeed = 5;
    float speed;
    public GameObject player;
    bool stop = true;
    public bool following = false;
    bool pMoving = false;
    Vector3 prevPpos;
    bool lerping = false;
    float x, prevx, y, prevy;
    Animator anim;
    bool left;
    bool up;
    bool moving;
    bool vert;
    bool prevMove;
    bool triggerland;
    float triggerLandCount;
    bool land;
    float speedBonus;
    public float followDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        landScript = GetComponent<animalLand>();
        prevPpos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        x = transform.position.x;
        y = transform.position.y;
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);
        if (x > prevx)
            left = false;
        if (x < prevx)
            left = true;
        if (y > prevy)
            up = true;
        if (y < prevy)
            up = false;

        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            vert = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            vert = true;
        }
        anim.SetBool("vert", vert);


        if (vert == true && y > prevy)
        {
            GetComponent<SpriteRenderer>().flipY = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (vert == true && y < prevy)
        {
            GetComponent<SpriteRenderer>().flipY = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (vert == false && x > prevx)
        {
            GetComponent<SpriteRenderer>().flipY = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(vert == false && x < prevx)
        {
            GetComponent<SpriteRenderer>().flipY = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }
                


        anim.SetBool("left", left);
        anim.SetBool("up", up);

        
        if (x != prevx || y != prevy)
        {
            moving = true;
            if (!prevMove)
            {
                anim.SetTrigger("moving 0");
            }
        }
        else
        {
            moving = false;
            if (prevMove)
            {
                anim.SetTrigger("idle");
            }
        }
        
        anim.SetBool("moving", moving);

       
        if (following && !stop)
        {
            moveGator();
        }
        //Debug.Log("lerping "+lerping);
        prevx = x;
        prevy = y;
        anim.SetFloat("prevx", prevx);
        anim.SetFloat("prevy", y);
        prevMove = moving;
        distanceCheck();
    }

    void moveGator()
    {
        //if(!lerping)
        // StartCoroutine(LerpPosition(3));
        gatormove2();
        anim.SetBool("land", land);
        Debug.Log(triggerLandCount);
    }

    void distanceCheck()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= followDistance)
        {
            stop = true;
            pMoving = true;
        }
        else
        {
            stop = false;
        }
    }


    void gatormove2()
    {


        landChecking();
        if (Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            speedBonus = Vector3.Distance(transform.position, player.transform.position)/4;
        }
        else
        {
            speedBonus = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed+speedBonus) * Time.deltaTime);



    }


    void landChecking()
    {
        if (triggerLandCount > 0 || landScript.LandCheck())
        {
            land = true;
            speed = landSpeed;
            //Debug.Log("land");
        }
        else
        {
            land = false;
            speed = waterSpeed;
        }
            

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "land")
        {
            //Debug.Log("Landed");
            //Debug.Log("Landed");
            triggerLandCount += 1;
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "land")
        {
            triggerLandCount -= 1;
            if (triggerLandCount < 0)
                triggerLandCount = 0;
        }
        
    }



    IEnumerator LerpPosition( float duration)
    {
        lerping = true;
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration && following && !stop)
        {
            
            float t = time / duration;
            if (landScript.LandCheck())
            {

                t =  (landSpeed * t);
            }
            else
            {
                t =   (waterSpeed * t);
            }

            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, t);
            time += Time.deltaTime;
            //Debug.Log(time);
            lerping = false;
            yield return null;
        }



    }




    


}
