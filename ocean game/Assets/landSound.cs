﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landSound : MonoBehaviour
{
    GameObject rainObj;
    rain rainScript;
    bool prevRain = false;

    public AudioClip landRainClip;
    public AudioClip landClip;
    AudioSource sor;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
        rainObj = GameObject.FindGameObjectWithTag("rain");
        rainScript = rainObj.GetComponent<rain>();
        if (rainScript.raining)
        {
            sor.time = Random.Range(0f, sor.clip.length);
            sor.clip = landRainClip;
            sor.Play();
        }
        else
        {
            sor.time = Random.Range(0f, sor.clip.length);
            sor.clip = landClip;
            sor.Play();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(prevRain != rainScript.raining)
        {
            if (rainScript.raining)
            {
                sor.time = Random.Range(0f, sor.clip.length);
                sor.clip = landRainClip;
                sor.Play();
            }
            else
            {
                sor.time = Random.Range(0f, sor.clip.length);
                sor.clip = landClip;
                sor.Play();
            }

        }


        prevRain = rainScript.raining;
    }
}