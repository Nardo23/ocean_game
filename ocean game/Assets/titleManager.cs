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

    AudioSource sor;
    [SerializeField]
    AudioClip[] windclips;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
