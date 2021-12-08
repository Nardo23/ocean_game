using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottled : MonoBehaviour
{
    AudioSource sor;
    public AudioClip bottleSound, map, splash;
    
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    public void playBottle()
    {
        sor.PlayOneShot(bottleSound);

    }

    public void playMap()
    {
        sor.PlayOneShot(map);
    }
    public void playSplash()
    {
        sor.PlayOneShot(splash);
    }

    
}
