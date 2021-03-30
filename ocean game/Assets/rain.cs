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
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rainParticles = GetComponent<ParticleSystem>();
        rainParticles.Stop();
        sor = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();
        vol = sor.volume;
        
        

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
        }
       
        

        prevRaining = raining;
    }
}
