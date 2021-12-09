using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EndOceanVol : MonoBehaviour
{
    public AudioMixer mixer;
    bool oceanPlay = true;

    // Update is called once per frame
    void Update()
    {
        if (oceanPlay)
        {
            playOcean();
        }
        else
        {
            muteOcean();
        }
    }

    void muteOcean()
    {
        float cur;
        mixer.GetFloat("OceanVol", out cur);
        mixer.SetFloat("OceanVol", Mathf.MoveTowards(cur, -80, 80 / 30 * Time.deltaTime));
    }
    void playOcean()
    {
        float cur;
        mixer.GetFloat("OceanVol", out cur);
        mixer.SetFloat("OceanVol", Mathf.MoveTowards(cur, 0, 80 / 30 * Time.deltaTime));
    }

    public void setMute()
    {
        oceanPlay = false;
    }
     public void setPlay()
    {
        oceanPlay = true;
    }   


}
