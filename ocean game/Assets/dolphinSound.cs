using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dolphinSound : MonoBehaviour
{
    public AudioSource sor;

    [SerializeField]
    AudioClip[] splashes;
    public AudioClip gentle1;
    public AudioClip gentle2;

    public AudioClip whaleSlosh;
    public AudioClip serpentSound;
    public AudioSource sor2;

    
    void playSplashes()
    {
        sor.pitch = Random.Range(.8f, 1.2f);
        sor.PlayOneShot(splashes[Random.Range(0, splashes.Length)]);

    }
    
    void playgentle1()
    {
        sor.pitch = Random.Range(.8f, 1.15f);
        sor.PlayOneShot(gentle1);
    }

    void playgentle2()
    {
        sor.pitch = Random.Range(.8f, 1.15f);
        sor.PlayOneShot(gentle2);
    }
    void playWhale()
    {
        sor.pitch = Random.Range(.85f, 1.15f);
        sor.PlayOneShot(whaleSlosh);
    }
    void playSerp()
    {
        sor2.pitch = Random.Range(.8f, 1.2f);
        sor2.PlayOneShot(serpentSound);
    }

}
