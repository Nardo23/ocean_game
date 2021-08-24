using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSound : MonoBehaviour
{
    public AudioClip clip1;
    public AudioSource sor;
    public AudioClip clip2;

    public void playClip1()
    {
        sor.PlayOneShot(clip1);
    }
    public void playClip2()
    {
        sor.PlayOneShot(clip2);
    }
}
