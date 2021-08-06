using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleAudio : MonoBehaviour
{
    public AudioClip spray;
    public AudioClip slosh;
    AudioSource sor;

    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    
    public void spraySound()
    {
        sor.PlayOneShot(spray);
        
    }
    public void sloshSound()
    {
        sor.PlayOneShot(slosh);
    }

}
