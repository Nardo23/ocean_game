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
    [SerializeField]
    AudioClip[] StoneSteps;
    [SerializeField]
    AudioClip[] SloshSteps;
    [SerializeField]
    AudioClip[] IceSteps;
    [SerializeField]
    AudioClip[] SnowSteps;
    [SerializeField]
    AudioClip[] GrassSteps;
    AudioSource Sor1;
    public AudioSource undergroundSor;
    public AudioSource houseSor;
    AudioSource Sor;



    //AudioReverbFilter reverb;
    public Vector2 pitchRange;
    public float sandVol;
    public float woodVol;
    public float stoneVol;
    public float sloshVol;
    public float iceVol;
    public float snowVol;
    public float grassVol;
    float range = 100;
    bool prevCreak = false;

    

    // Start is called before the first frame update
    void Start()
    {
        Sor1 = GetComponent<AudioSource>();
        //reverb = GetComponent<AudioReverbFilter>();
        range = 100;
    }

    void Step()
    {
        if (playerScript.inUnderworld && playerScript.sfxTerrainType !=5 && !playerScript.inHouse)
        {
            Sor = undergroundSor;
           // reverb.enabled = true;
        }
        else if (playerScript.inUnderworld && playerScript.sfxTerrainType != 5 && playerScript.inHouse)
        {
            Sor = houseSor;
            // reverb.enabled = true;
        }
        else
        {
            Sor = Sor1;
           // reverb.enabled = false;
        }


        if (playerScript.sfxTerrainType == 1) //sand
        {
            Sor.volume = sandVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(SandSteps[UnityEngine.Random.Range(0, SandSteps.Length)]);

        }
        if (playerScript.sfxTerrainType == 2)//grass
        {
            Sor.volume = grassVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(GrassSteps[UnityEngine.Random.Range(0, GrassSteps.Length)]);
        }

        if (playerScript.sfxTerrainType == 3)//stone
        {
            Sor.volume = stoneVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            int clip = UnityEngine.Random.Range(0, StoneSteps.Length);
            if (clip == 2 && UnityEngine.Random.Range(0, 1) == 1)
            {
                clip = UnityEngine.Random.Range(0, StoneSteps.Length);
            }

            Sor.PlayOneShot(StoneSteps[clip]);
        }


        if (playerScript.sfxTerrainType == 4) // wood
        {
            Sor.volume = woodVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(WoodSteps[UnityEngine.Random.Range(0, WoodSteps.Length)]);
            if (!playerScript.inUnderworld)
            {
                float i = Random.Range(1, range);
                if (i <= 30 && !prevCreak)
                {
                    prevCreak = true;
                    range += 3;
                    if (range > 120)
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
            }           
            //Debug.Log(range);
        }
        if (playerScript.sfxTerrainType == 5)//slosh
        {
            Sor.volume = sloshVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(SloshSteps[UnityEngine.Random.Range(0, SloshSteps.Length)]);
        }
        if (playerScript.sfxTerrainType == 6)//ice
        {
            Sor.volume = iceVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(IceSteps[UnityEngine.Random.Range(0, IceSteps.Length)]);
        }
        if (playerScript.sfxTerrainType == 7)//snow
        {
            Sor.volume = snowVol;
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(SnowSteps[UnityEngine.Random.Range(0, SnowSteps.Length)]);
        }
    }




    // Update is called once per frame
   
}
