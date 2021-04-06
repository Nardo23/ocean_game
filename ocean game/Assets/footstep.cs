using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstep : MonoBehaviour
{
    public Player playerScript;
    [SerializeField]
    AudioClip[] SandSteps;
    [SerializeField]
    AudioClip[] WoodSteps;
    [SerializeField]
    AudioClip[] Creak;
    AudioSource Sor;
    AudioReverbFilter reverb;
    public Vector2 pitchRange;
    public float sandVol;
    public float woodVol;
    float range = 100;
    bool prevCreak = false;

    // Start is called before the first frame update
    void Start()
    {
        Sor = GetComponent<AudioSource>();
        reverb = GetComponent<AudioReverbFilter>();
        range = 100;
    }

    void Step()
    {
        if (playerScript.inUnderworld)
        {
            reverb.enabled = true;
        }
        else
        {
            reverb.enabled = false;
        }


        if (playerScript.sfxTerrainType == 1) //sand
        {
            Sor.volume = sandVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(SandSteps[UnityEngine.Random.Range(0, SandSteps.Length)]);

        }
        if (playerScript.sfxTerrainType == 4) // wood
        {
            Sor.volume = woodVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(WoodSteps[UnityEngine.Random.Range(0, WoodSteps.Length)]);
            float i = Random.Range(1, range); 
            if (i <=30 && !prevCreak)
            {
                prevCreak = true;
                range += 3;
                if(range>120)
                {
                    range = 120;
                }
                Sor.PlayOneShot(Creak[UnityEngine.Random.Range(0, Creak.Length)]);
            }
            else
            {
                if (!prevCreak)
                {
                    range -= 3;
                    if (range < 80)
                    {
                        range = 80;
                    }
                }
                prevCreak = false;
                
            }
           //Debug.Log(range);
        }
    }




    // Update is called once per frame
   
}
