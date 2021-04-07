using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfallAudio : MonoBehaviour
{
    AudioSource sor;
    public AudioClip clip;
    bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();    
        sor.time = Random.Range(0f, sor.clip.length);
        sor.clip = clip;
        sor.Play();
        started = true;
    }
    private void OnEnable()
    {
        if (started)
        {
            sor.time = Random.Range(0f, sor.clip.length);
            sor.clip = clip;
            sor.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
