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

    public bool cameraTransition = true;
    public Color camColor = new Color(255f, 255f, 255f, 255f);
    public GameObject camTranObj;

    public float lerpSpeed = 4;
    private float startTime;
    private float journeyLength;
    public bool forceLand = false;

    public bool sound = false;
    AudioSource sor;
    public float highPitch = 1.7f, lowPitch = .4f;
    public AudioClip lerpClip;
    public AudioClip arriveClip;
    bool volDown = false;
    public AudioSource doorSor;
    public ParticleSystem arriveEffect;
    // Start is called before the first frame update
    void Start()
    {
        
        NewExit = new Vector3 (ExitOffset.x+Exit.position.x, ExitOffset.y+ Exit.position.y, 0f);
        journeyLength = Vector3.Distance(Playerposition.position, NewExit);

        if (sound)
        {
            sor = GetComponent<AudioSource>();
        }
        
    }

    IEnumerator lerpVol(float duration)
    {
        float time = 0;
        while (sor.volume>=.05f)
        {
            time += Time.deltaTime;
            sor.volume= Mathf.Lerp(sor.volume, 0, Time.deltaTime*duration);

            yield return null;
        }
        sor.volume = 0;   
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
        if (sound)
        {
            sor.PlayOneShot(lerpClip);
            sor.volume = 1;
        }
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            Playerposition.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            //Debug.Log(time);

            if (sound)
            {
                sor.pitch = Mathf.Lerp(highPitch, lowPitch, .3f*time);
                if(sor.time >= .9f)
                {
                    sor.PlayOneShot(lerpClip);
                }
                if (Vector2.Distance(Playerposition.position, targetPosition) <= Vector2.Distance(startPosition, targetPosition) * .2f && volDown == false) 
                {
                    volDown = true;
                    //Debug.Log("yess");
                    StartCoroutine(lerpVol(3));
                    if (arriveClip != null)
                    {
                        doorSor.pitch = 1;
                        doorSor.PlayOneShot(arriveClip, 1);
                    }
                }
            }

                yield return null;
        }

        Playerposition.position = targetPosition;
        if (sound)
        {
            volDown = false;
            //sor.Stop();
            
            
        }
        if(arriveEffect != null)
        {
            arriveEffect.Play();
        }

        playerScript.canMove = true;
        col.enabled = true;
        if (invis)
        {
            rend.enabled = true;
            headRend.enabled = true;
            sailRend.enabled = true;
        }

    }


    private IEnumerator Countdown()
    {
        float duration = 1f; 
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        playerScript.canMove = true;
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
            playerScript.triggerCaveWater = false;


            //Debug.Log("Piss");
            if (lerpDoor)
            {
                startTime = Time.time;
                playerScript.canMove = false;
                startLerp = true;

            }
            else
            {
                if (cameraTransition)
                {
                    camTranObj.GetComponent<SpriteRenderer>().color = camColor;
                    camTranObj.SetActive(true);
                }
                Playerposition.position = NewExit;
                playerScript.playerDirection = new Vector2(0f, 0f);
            }
            
            
            if (changeWorld)
            {
                


                
                playerScript.SwapWorld();
                playerScript.doorStore = transform.position;
                playerScript.inHouse = false;
                if(forceLand)
                    playerScript.triggerLandCount = 2;
                else
                    playerScript.triggerLandCount = 0;
            }
           
           
        }
            
    }

}
