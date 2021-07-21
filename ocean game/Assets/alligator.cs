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
    Player playerScript;
    public GameObject target;
    public float Xoffset, yOffset;
    public GameObject Destination;
    bool stop = false;
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
    bool caveWater;

    bool land;
    float speedBonus;
    public float followDistance = 3f;
    bool homeZone = false;
    public bool arrived = false;
    public whereAreMyGators whereAreGatorsScript;
    bool started = false;
    public GameObject barrier;
    npcDialogue npcScript;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        landScript = GetComponent<animalLand>();
        prevPpos = target.transform.position;
        playerScript = player.GetComponent<Player>();
        npcScript = GetComponent<npcDialogue>();
        

    }

    void getState()
    {
        Debug.Log("gatorLoad");
        if (playerScript.gatorState == 0)
        {
            following = false;
        }
        else if (playerScript.gatorState == 1)
        {
            following = true;
            barrier.SetActive(false);
        }
        else if (playerScript.gatorState == 2)
        {
            transform.position = Destination.transform.position;
            arrived = true;
            whereAreGatorsScript.gatorArrived();
            npcScript.heart = true;
            following = false;
            barrier.SetActive(false);
        }
    }
    void homeMove()
    {

        anim.SetBool("land", land);
        transform.position = Vector2.MoveTowards(transform.position, Destination.transform.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, Destination.transform.position) < .35f)
        {
            transform.position = Destination.transform.position;
            arrived = true;
            whereAreGatorsScript.gatorArrived();
            npcScript.heart = true;
        }
    }

    void tick()
    {

        if (following && !arrived)
        {
            playerScript.gatorState = 1;
        }



        if (!arrived)
        {
            if (player.GetComponent<Player>().inUnderworld)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
            }
            if (homeZone)
            {
                homeMove();
            }
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
            else if (vert == false && x < prevx)
            {
                GetComponent<SpriteRenderer>().flipY = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (!vert)
            {
                GetComponent<SpriteRenderer>().flipY = false;
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
        else
        {
            vert = false;
            left = true;
            moving = false;
            anim.SetBool("land", true);
            anim.SetBool("left", left);
            anim.SetBool("vert", vert);
            anim.SetBool("moving", false);
            anim.ResetTrigger("moving 0");
            anim.SetTrigger("idle");
            GetComponent<SpriteRenderer>().flipY = false;
            GetComponent<SpriteRenderer>().flipX = false;
            playerScript.gatorState = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            
            
            if (playerScript.loaded)
            {
                getState();
                started = true;
            }
        }
        else
        {
            tick();
        }

        

    }

    void moveGator()
    {
        //if(!lerping)
        // StartCoroutine(LerpPosition(3));
        gatormove2();
        anim.SetBool("land", land);
        //Debug.Log(triggerLandCount);
    }

    void distanceCheck()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= followDistance)
        {
            stop = true;
            pMoving = true;
        }
        /*
        
        else if (Vector3.Distance(transform.position, target.transform.position) > 33 && following)
        {
            transform.position = new Vector3(target.transform.position.x + 3+Xoffset, target.transform.position.y+yOffset, transform.position.z);
        }
        */
        else
        {
            stop = false;
        }
    }


    void gatormove2()
    {


        landChecking();
        if (Vector3.Distance(transform.position, new Vector3(target.transform.position.x+ Xoffset, target.transform.position.y+yOffset, target.transform.position.z)) > 8)
        {
            speedBonus = Vector3.Distance(transform.position, target.transform.position)/4;
        }
        else if ((Vector3.Distance(transform.position, new Vector3(target.transform.position.x + Xoffset, target.transform.position.y + yOffset, target.transform.position.z)) > 33))
        {
            speedBonus = 30;
        }
        else
        {
            speedBonus = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, (speed+speedBonus) * Time.deltaTime);



    }


    void landChecking()
    {
        
        
        if (triggerLandCount > 0 || landScript.LandCheck())
        {
            land = true;
            speed = landSpeed;
            //Debug.Log("land");
        }
        else if (player.GetComponent<Player>().inUnderworld)
        {
            if (caveWater)
            {
                speed = waterSpeed;
                land = false;
            }
            else
            {
                land = true;
                speed = landSpeed;
            }
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
        if (other.tag == "caveWater")
        {
            caveWater = true;

        }
        if(other.tag == "destination")
        {
           
            following = false;
            homeZone = true;
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
        if (other.tag == "caveWater")
        {
            caveWater = false;

        }
        if (other.tag == "destination")
        {

            following = true;
            //homeZone = false;
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
            transform.position = Vector3.Lerp(transform.position, target.transform.position, t);
            time += Time.deltaTime;
            //Debug.Log(time);
            lerping = false;
            yield return null;
        }



    }




    


}
