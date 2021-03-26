using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rain : MonoBehaviour
{
    public Animator weatherAnim;
    public bool raining = false;
    private ParticleSystem rainParticles;
    
    // Start is called before the first frame update
    void Start()
    {
        rainParticles = GetComponent<ParticleSystem>();
        rainParticles.Stop();


    }
    void SetRain()
    {
        weatherAnim.SetBool("rain", true);
        rainParticles.Play();

    }
    void EndRain()
    {
        weatherAnim.SetBool("rain", false);
        rainParticles.Stop();
    }


    // Update is called once per frame
    void Update()
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
}
