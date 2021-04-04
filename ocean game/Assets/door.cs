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

    public float lerpSpeed = 4;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        NewExit = new Vector3 (ExitOffset.x+Exit.position.x, ExitOffset.y+ Exit.position.y, 0f);
        journeyLength = Vector3.Distance(Playerposition.position, NewExit);
        
    }

    private void FixedUpdate()
    {
        if (startLerp)
        {
            float distCovered = (Time.time-startTime) *lerpSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            Playerposition.position = Vector3.Lerp(Playerposition.position, NewExit, fractionOfJourney);
        }
        if(Playerposition.position == NewExit)
        {
            startLerp = false;
            playerScript.canMove = true;
        }
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
                
            }

        }
            
    }

}
