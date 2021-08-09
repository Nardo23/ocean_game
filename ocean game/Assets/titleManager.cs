using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleManager : MonoBehaviour
{
    public GameObject upObj, leftObj, downObj, rightObj;
    public GameObject uPs, dPs, rPs, lPs;
    public GameObject leftkey, rightkey, upkey, downkey;
    float horizontal, vertical;
    bool u = false, d = false, l = false, r = false;
    bool canplay = true;
    float timer = 0f;
    float endTimer = 0f;
    public Animator waterAnim;
    AudioSource sor;
    [SerializeField]
    AudioClip[] windclips;
    [SerializeField]
    AudioClip[] plucks;
    int i = 0;
    bool first = false;
    bool chimed = false;
    public AudioClip chime;
    public AudioClip bigWave;
    public AudioSource pSor;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (u && d && r && l)
        {
            if (!chimed)
            {
                chimed = true;
                pSor.pitch = 1f;
                pSor.PlayOneShot(chime);
            }
            endTimer += Time.deltaTime;
            if (endTimer > 1.2f && !first)
            {
                first = true;
                waterAnim.SetTrigger("rinse");
                sor.pitch = 1;
                sor.PlayOneShot(bigWave);
            }
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Debug.Log("Horizontal: " + horizontal);
        //Debug.Log("Vertical: " + vertical);
        if(horizontal > .1f)
        {
            r = true;
            rightkey.GetComponent<Animator>().SetTrigger("press");
        }
        else if (horizontal < -.1f)
        {
            l = true;
            leftkey.GetComponent<Animator>().SetTrigger("press");
        }
        else if (vertical > .1f)
        {
            u = true;
            upkey.GetComponent<Animator>().SetTrigger("press");
        }
        else if (vertical < -.1f)
        {
            d = true;
            downkey.GetComponent<Animator>().SetTrigger("press");
        }

        //Debug.Log(canplay);
        if (!canplay)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
        }
        if (timer > .35f)
        {
            canplay = true;
            timer = 0f;
        }



        if (canplay)
        {
            if (u)
            {
                playUp();
            }
            if (d)
            {
                playDown();
            }
            if (l)
            {
                playLeft();
            }
            if (r)
            {
                playRight();
            }
        }

    }

    public void playRight()
    {
        if(rightObj.activeSelf == false)
        {
            gustSound();
            canplay = false;
            pSor.PlayOneShot(plucks[i]);
            i += 1;
            
        }
        rightObj.SetActive(true);
        rPs.SetActive(true);
    }

    public void playLeft()
    {
        if (leftObj.activeSelf == false)
        {
            gustSound();
            canplay = false;
            pSor.PlayOneShot(plucks[i]);
            i += 1;
        }
        leftObj.SetActive(true);
        lPs.SetActive(true);
    }
    public void playUp()
    {
        if (upObj.activeSelf == false)
        {
            gustSound();
            canplay = false;
            pSor.PlayOneShot(plucks[i]);
            i += 1;
        }
        upObj.SetActive(true);
        uPs.SetActive(true);

    }
    public void playDown()
    {
        if (downObj.activeSelf == false)
        {
            gustSound();
            canplay = false;
            pSor.PlayOneShot(plucks[i]);
            i += 1;
        }
        downObj.SetActive(true);
        dPs.SetActive(true);
    }

    public void gustSound()
    {
        sor.pitch = Random.Range(.8f, 1.2f);
        sor.PlayOneShot(windclips[Random.Range(0, windclips.Length)]);

    }


}
