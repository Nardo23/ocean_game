using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableObj : MonoBehaviour
{
    public GameObject obj;
    public GameObject cam;
    public GameObject player;
    Player playerScript;
    public float lerpTime;
    public float lookTime;

    AudioSource sor;
    public AudioClip poof;

    bool quit = false;
    bool first = false;
    bool second = false;

    public bool enableOrDisable = true;
    public bool worldChange = false;

    bool revealed = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        sor = GetComponent<AudioSource>();
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        //Debug.Log("StartLerp");
        float time = 0;
        Vector3 startPosition = cam.transform.position;



        while (time < duration)
        {
            if (second)
            {
                targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, cam.transform.position.z);
            }
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            //Debug.Log(time);
            yield return null;
        }
        cam.transform.position = targetPosition;
        obj.SetActive(enableOrDisable);
        
        if (first)
        {
            StartCoroutine(wait(lookTime));
            sor.PlayOneShot(poof);
        }
        else
        {
            //Debug.Log("weeeppe");
            playerScript.canMove = true;
            cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cam.transform.position.z);
        }
    }

    IEnumerator wait(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            //Debug.Log("We have waited for: " + counter + " seconds");
            if (quit)
            {
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            if (worldChange && first ==  true)
            {
                playerScript.SwapWorld();
            }
            first = false;
            Vector3 camPos = new Vector3(player.transform.position.x, player.transform.position.y, cam.transform.position.z);
            
            StartCoroutine(LerpPosition(camPos, lerpTime));
            second = true;

            yield return null;
        }
    }

    void waitTime(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !revealed && !playerScript.inUnderworld)
        {
            revealed = true;
            playerScript.canMove = false;
            first = true;
            Vector3 camPos = new Vector3 (obj.transform.position.x, obj.transform.position.y, cam.transform.position.z);
            if (worldChange)
            {
                playerScript.SwapWorld();
            }
            StartCoroutine(LerpPosition(camPos, lerpTime));
            //waitTime(lookTime+lerpTime);

        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = true;

        }
    }

}
