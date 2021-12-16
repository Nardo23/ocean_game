﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class endArea : MonoBehaviour
{
    public GameObject player;
    float timer =0;
    public GameObject reflectStuff;
    public Weather weatherScript;
    public rain rainScript;
    Player playerScript;
    waterColor waterColorScript;
    public Animator Sun;
    public float weatherEnd, reflectionTime = 30, sunsetStart, lastSongTime = 60, bottleTIme= 120, LouderTime = 100f;
    bool weatherEnded = false, sunsetStarted = false, lastSong =false, louder = false, bottled = false;
    bool reflect = false;
    public Animator overworldAnim;
    public GameObject SerpentSpawner;

    public map mapScript;
    public GameObject mapBg;
    public GameObject bottleObj;
    public GameObject head;

    bool endLoop = false;
    bool canEnd = false;

    public AudioMixer mixer;
    private AudioMixerSnapshot startingSnap;
    private AudioMixerSnapshot endingSnap;
    public float reverbChangeTime =2f;
    public float startVol, endVol, volSpeed;
    public AudioClip EndSong;
    public AudioSource sor;

    float endTimer = 0f;
   

    // Start is called before the first frame update
    void Start()
    {
        startingSnap = mixer.FindSnapshot("SnapshotDefault");
        endingSnap = mixer.FindSnapshot("Snapshot2");
        mixer.SetFloat("MusicVol", startVol);
        playerScript = player.GetComponent<Player>();
        weatherScript.enabled = false;
        waterColorScript = GetComponent<waterColor>();
    }

    // Update is called once per frame
    void Update()
    {
        timeTick();
        

    }

    void timeTick()
    {
        if (timer < 60 * 10)// stop increasing timer after 10 minutes just in case 
        {
            timer += Time.deltaTime;

            if (timer >= weatherEnd && !weatherEnded)
            {
                weatherEnded = true;
                endWeather();
                
            }
            else if (timer >= sunsetStart && !sunsetStarted)
            {
                sunsetStarted = true;
                sunset();
            }
            else if (timer >= reflectionTime && reflect == false)
            {
                reflectStuff.SetActive(true);
                reflect = true;
                SerpentSpawner.SetActive(true);
            }
            else if (timer >= lastSongTime && !lastSong)
            {
                lastSong = true;
                sor.clip = EndSong;
                sor.loop = true;
                sor.Play();

            }
            else if(timer >= LouderTime && !louder)
            {
                louder = true;
                ChangeVol();
            }
            else if (timer >= bottleTIme && !bottled && !mapBg.activeSelf)
            {
                bottled = true;
                mapScript.endingState();
            }

        }

        if (louder)
        {
            ChangeVol();
        }
        if (endLoop)
        {
            endLoopStuff();
        }

    }

    void endLoopStuff()
    {
        if (canEnd)
        {
            sor.loop = false;
            if (!sor.isPlaying)
            {
                if (endTimer < 2)
                {
                    endTimer += Time.deltaTime;
                }
                else
                {
                    endGame();
                }
                
            }
        }   
        else if (EndSong.length - sor.time >= 22f)
        {
            canEnd = true;
        }
    }

    void endGame()
    {
        Debug.Log("Goodbye World");
    }


    void endWeather()
    {
        playerScript.windSpeed = 0;
        playerScript.setWindDirection(playerScript.WindDirect);
        rainScript.raining = false;

    }

    void sunset()
    {
        Sun.SetTrigger("endLight");
        overworldAnim.SetBool("day", true);
        waterColorScript.setColorFromScript();
    }


    public void bottle()
    {
        playerScript.canMove = false;
        SerpentSpawner.SetActive(false);
        SpriteRenderer pee = playerScript.gameObject.GetComponent<SpriteRenderer>();
        head.SetActive(false);
        if (pee.flipX)
        {
            bottleObj.GetComponent<SpriteRenderer>().flipX = true;
        }
        bottleObj.SetActive(true);
        pee.enabled = false;
        ChangeReverb();
        endLoop = true;
        sor.loop = true;
        if (EndSong.length - sor.time < 22f)
        {
            canEnd = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    void ChangeVol()
    {
        float cur;
        mixer.GetFloat("MusicVol", out cur);
        mixer.SetFloat("MusicVol", Mathf.MoveTowards(cur, endVol, (Mathf.Abs(startVol)+ endVol)/30 * Time.deltaTime));
        mixer.GetFloat("LowPassFreq", out cur);
        mixer.SetFloat("LowPassFreq", Mathf.MoveTowards(cur, 22000f, volSpeed*724.2f * Time.deltaTime));
    }


    void ChangeReverb()
    {
        endingSnap.TransitionTo(reverbChangeTime);


    }

    
}
