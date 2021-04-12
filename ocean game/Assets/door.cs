using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Transform Exit;
    public Transform Playerposition;
    public Vector2 ExitOffset;
    private Vector3 NewExit;
    public Player playerScript;
    public bool changeWorld;
    public bool lerpDoor = false;
    private bool startLerp = false;
    public bool invis = false;
    public SpriteRenderer rend;
    public SpriteRenderer headRend;
    public SpriteRenderer sailRend;
    public Collider2D col;

    public float lerpSpeed = 4;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        
        NewExit = new Vector3 (ExitOffset.x+Exit.position.x, ExitOffset.y+ Exit.position.y, 0f);
        journeyLength = Vector3.Distance(Playerposition.position, NewExit);
        
    }


    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        //Debug.Log("StartLerp");
        float time = 0;
        Vector3 startPosition = Playerposition.position;
        col.enabled = false;
        if (invis)
        {
            headRend.enabled = false;
            rend.enabled = false;
            sailRend.enabled = false;
        }

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            Playerposition.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            //Debug.Log(time);
            yield return null;
        }
        Playerposition.position = targetPosition;
        
        playerScript.canMove = true;
        col.enabled = true;
        if (invis)
        {
            rend.enabled = true;
            headRend.enabled = true;
            sailRend.enabled = true;
        }

    }



    private void FixedUpdate()
    {
        if (startLerp)
        {
            startLerp = false;
            StartCoroutine(LerpPosition(NewExit, lerpSpeed));
            
        }


        /*
        if (startLerp)
        {
            col.enabled = false;
            if (invis)
            {
                rend.enabled = false;
            }
            float distCovered = (Time.time-startTime) *lerpSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            Playerposition.position = Vector3.Lerp(Playerposition.position, NewExit, fractionOfJourney);
        }
        if (Playerposition.position == NewExit && startLerp)
        {
            print("arrived!");
            startLerp = false;
            playerScript.canMove = true;
            col.enabled = true;
            if (invis)
            {
                rend.enabled = true;
            }
        }

        */


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Piss");
            if (lerpDoor)
            {
                startTime = Time.time;
                playerScript.canMove = false;
                startLerp = true;

            }
            else
            {
                Playerposition.position = NewExit;
            }
            
            
            if (changeWorld)
            {
                playerScript.SwapWorld();
                playerScript.doorStore = transform.position;
                
            }

        }
            
    }

}
