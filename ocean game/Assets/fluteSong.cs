using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluteSong : MonoBehaviour
{
    AudioSource sor;
    AudioLowPassFilter lowPass;
    public float outsideLowFreq =1200;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();
        sor.Play();


        outSide();
    }

    

    public void outSide()
    {
        sor.spatialBlend = 1;
        lowPass.enabled = true;
        lowPass.cutoffFrequency = 1200;
    }

    public void inSide(float vol, bool lowOn, float lowFreq)
    {
        sor.spatialBlend = 0;
        sor.volume = vol;
        lowPass.cutoffFrequency = lowFreq;
        if (lowOn)
        {
            lowPass.enabled = true;
        }
        else
        {
            lowPass.enabled = false;
        }
    }

 

}
