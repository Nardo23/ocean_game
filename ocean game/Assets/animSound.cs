using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSound : MonoBehaviour
{
    public AudioClip clip1;
    public AudioSource sor;
    public AudioClip clip2;
    [SerializeField]
    AudioClip[] clips;
    public void playClip1()
    {
        sor.pitch = 1;
        sor.PlayOneShot(clip1);
    }
    public void playClip2()
    {
        sor.pitch = 1;
        sor.PlayOneShot(clip2);
    }

    public void PlayRandomClip()
    {
        sor.pitch = Random.Range(.8f, 1.2f);
        sor.PlayOneShot(clips[Random.Range(0, clips.Length)], .3f);
    }

}
