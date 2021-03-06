using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class plantWave : MonoBehaviour
{
    public GameObject waterMask;
    public ParticleSystem Ripples;
    plantSounds plantSoundsScript;
    public AudioSource sor;
    AudioClip[] clip;
     float pVolume;
    Vector2 pitchRange;
    Player playerScript;
    AudioReverbFilter filter;
    public bool isPlayer = true;

    private void Start()
    {
        filter = sor.GetComponent<AudioReverbFilter>();
        if (isPlayer)
            playerScript = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if (playerScript.inUnderworld)
            {
                filter.enabled = true;
            }
            else
            {
                filter.enabled = false;
            }
        }      
        


        if (collision.gameObject.tag == "caveWater" && isPlayer)
        {
            waterMask.SetActive(true);
            Ripples.Play();
        }
        if (collision.gameObject.tag == "plant")
        {
            if (collision.GetComponent<plantSounds>() != null)
            {
                plantSoundsScript = collision.GetComponent<plantSounds>();
                clip = plantSoundsScript.getSmoosh();
                if (!sor.isPlaying)
                {
                    pitchRange = plantSoundsScript.getPitchRange();
                    sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
                }
                pVolume = plantSoundsScript.getVolume();
                sor.PlayOneShot(clip[UnityEngine.Random.Range(0, clip.Length)], pVolume);

            }
          
            GameObject plant = collision.gameObject;
            Animator anim = plant.GetComponent<Animator>();

            anim.SetTrigger("wave");
            

        }
    }

    public void endWater()
    {
        waterMask.SetActive(false);
        Ripples.Stop();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "caveWater" && isPlayer)
        {
            waterMask.SetActive(false);
            Ripples.Stop();
        }
        if (collision.gameObject.tag == "plant")
        {
            if (collision.GetComponent<plantSounds>() != null)
            {
                plantSoundsScript = collision.GetComponent<plantSounds>();
                clip = plantSoundsScript.getRelease();
                if (!sor.isPlaying)
                {
                    pitchRange = plantSoundsScript.getPitchRange();
                    sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
                }
                    
                pVolume = plantSoundsScript.getVolume();
                sor.PlayOneShot(clip[UnityEngine.Random.Range(0, clip.Length)], pVolume);
            }

            GameObject plant = collision.gameObject;
            Animator anim = plant.GetComponent<Animator>();

            anim.SetTrigger("reset");
            
        }
    }

}
