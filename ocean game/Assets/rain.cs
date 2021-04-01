using System.Collections;
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
    
    
    // Start is called before the first frame update
    void Start()
    {
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
        
        
        sor.Play();
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
    void EndRain()
    {
        weatherAnim.SetBool("rain", false);
        rainParticles.Stop();
        ripples.SetActive(false);
        sor.Stop();
        caveRain.SetActive(false);
    }




    // Update is called once per frame
    void Update()
    {
        if(wasDay != weatherScript.day)
        {
            Debug.Log(weatherScript.day);
            weatherAnim.SetBool("day",weatherScript.day);
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
            rainParticles.Stop();
        }
        else if (wasInUnderworld && playerScript.inUnderworld == false && raining == true )
        {
            wasInUnderworld = false;
            caveRain.SetActive(false);
            
            lowPass.enabled = false;
            rainParticles.Play();
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
                Debug.Log("p1");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("day grid", 0, (weatherScript.dayLength - weatherScript.timeRemaining) / 20);
            }
            else if (weatherScript.timeRemaining > weatherScript.nightLength - 20f && !weatherScript.day)
            {
                Debug.Log("p2");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("nightGrid", 0, (weatherScript.nightLength - weatherScript.timeRemaining) / 20);
            }
            else if (weatherScript.day)
            {
                Debug.Log("p3");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("dayGrid idle", 0, 0);
            }
            else if (!weatherScript.day)
            {
                Debug.Log("p4");
                weatherAnim.SetBool("day", weatherScript.day);
                weatherAnim.SetBool("rain", raining);
                weatherAnim.Play("night grid idle", 0, 0);
            }
        }


        wasDay = weatherScript.day;
        prevRaining = raining;
    }
}
