using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stilledWind : MonoBehaviour
{
    AudioHighPassFilter highPass;
    AudioSource mainSor;
    public AudioSource sor1, sor2;

    // Start is called before the first frame update
    void Start()
    {
        highPass = GetComponent<AudioHighPassFilter>();
        mainSor = GetComponent<AudioSource>();
    }
    public void caveSor()
    {
        sor1.enabled = false;
        sor2.enabled = true;
        
        sor2.timeSamples = mainSor.timeSamples;
        mainSor.enabled = false;

    }

    public void boatSor()
    {
        sor2.enabled = false;
        sor1.enabled = true;
        
        sor1.timeSamples = mainSor.timeSamples;
        mainSor.enabled = false;

    }

    public void off()
    {
        if (sor2.enabled)
        {
            mainSor.timeSamples = sor2.timeSamples;
        }
        else if (sor1.enabled)
        {
            mainSor.timeSamples = sor1.timeSamples;
        }
        mainSor.enabled = true;
        sor2.enabled = false;
        sor1.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            off();
        }
    }


}
