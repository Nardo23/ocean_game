﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rain : MonoBehaviour
{
    public Animator weatherAnim;
    public bool raining = false;
    bool prevRaining = false;
    private ParticleSystem rainParticles;
    public GameObject ripples;
    public GameObject caveRain;
    AudioSource sor;
    AudioLowPassFilter lowPass;
    public Player playerScript;
    private float vol;
    bool wasInUnderworld = false;
    public Weather weatherScript;
    private bool wasDay = true;
    public ParticleSystem snow;
    public bool snowMode;
    bool prevSnowing = false;

    public bool storm;
    public GameObject lightning;
    // Start is called before the first frame update
    void Start()
    {
        lightning.SetActive(false);
        rainParticles = GetComponent<ParticleSystem>();
        rainParticles.Stop();
        sor = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();
        vol = sor.volume;

        weatherAnim.SetBool("day", weatherScript.day);

    }
    void SetRain()
    {
        weatherAnim.SetBool("rain", true);
        
        if(!snowMode)
        {
            sor.Play();
        }
        else
        {
            prevSnowing = true;
        }
        
        if (playerScript.inUnderworld)
        {
            caveRain.SetActive(true);
            lowPass.enabled = true;
            sor.volume = vol / 2;
        }
        else
        {
            ripples.SetActive(true);
            rainParticles.Play();
            lowPass.enabled = false;
            sor.volume = vol;
        }

    }
    public void EndRain()
    {
        weatherAnim.SetBool("rain", false);
        rainParticles.Stop();
        ripples.SetActive(false);
        sor.Stop();
        caveRain.SetActive(false);
        prevSnowing = false;
    }




    // Update is called once per frame
    void Update()
    {
        if (raining && storm)
        {
            lightning.SetActive(true);
            //Debug.Log("ssss");
        }
        else
        {
            lightning.SetActive(false);
        }
        if (prevSnowing != playerScript.snow)
        {
            

            if (playerScript.snow)
            {
                rainParticles.Stop();
                rainParticles = snow;
                snowMode = true;

            }
            else
            {
                rainParticles.Stop();
                snowMode = false;
                rainParticles = GetComponent<ParticleSystem>();
                if (raining)
                {
                    SetRain();
                }
            }
        }
        

        if(wasDay != weatherScript.day)
        {
            //Debug.Log(weatherScript.day);
            weatherAnim.SetBool("day",weatherScript.day);
        }

        if(raining && prevSnowing != snowMode)
        {
            EndRain();
            SetRain();
        }

        if(prevRaining!= raining)
        {
            if (raining)
            {
                SetRain();
            }
            else
            {
                EndRain();
            }
        }
        if (playerScript.inUnderworld == true && raining)
        {
            wasInUnderworld = true;
            lowPass.enabled = true;
            caveRain.SetActive(true);
            ripples.SetActive(false);
            sor.volume = vol / 2;
            rainParticles.gameObject.transform.position = new Vector3(rainParticles.gameObject.transform.position.x, rainParticles.gameObject.transform.position.y, -1000);
            rainParticles.Stop();
        }
        else if (wasInUnderworld && playerScript.inUnderworld == false && raining == true )
        {
            wasInUnderworld = false;
            caveRain.SetActive(false);
            
            lowPass.enabled = false;
            rainParticles.Play();
            rainParticles.gameObject.transform.position = new Vector3(rainParticles.gameObject.transform.position.x, rainParticles.gameObject.transform.position.y, 0);
            ripples.SetActive(true);  
            if (weatherScript.day)
            {
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("rain", 0, 0);
            }
            else
            {
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("night rain", 0, 0);
            }

        }
        if (playerScript.inUnderworld == true && !raining)
        {
            wasInUnderworld = true;
            

        }
        if (wasInUnderworld && playerScript.inUnderworld == false && !raining) //set animator state to last state before underworld
        {
            wasInUnderworld = false;
            if (weatherScript.timeRemaining > weatherScript.dayLength - 20f && weatherScript.day)
            {
                //Debug.Log("p1");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("day grid", 0, (weatherScript.dayLength - weatherScript.timeRemaining) / 20);
            }
            else if (weatherScript.timeRemaining > weatherScript.nightLength - 20f && !weatherScript.day)
            {
                //Debug.Log("p2");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("nightGrid", 0, (weatherScript.nightLength - weatherScript.timeRemaining) / 20);
            }
            else if (weatherScript.day)
            {
                //Debug.Log("p3");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("dayGrid idle", 0, 0);
            }
            else if (!weatherScript.day)
            {
                //Debug.Log("p4");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("night grid idle", 0, 0);
            }
        }


        wasDay = weatherScript.day;
        prevRaining = raining;
    }
}
