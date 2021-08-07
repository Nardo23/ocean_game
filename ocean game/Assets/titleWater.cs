using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleWater : MonoBehaviour
{
    public GameObject wetSand;
    AudioSource sor;
    [SerializeField] AudioClip[] waveClips;
    public Vector2 pitchRange;
    AudioLowPassFilter low;
    private void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    public void setPosition()
    {
        wetSand.transform.position = transform.position;
    }

    public void waveSound()
    {
        sor.pitch = Random.Range(pitchRange.x, pitchRange.y);
        sor.PlayOneShot(waveClips[Random.Range(0, waveClips.Length)]);
    }


    public void increaseLow()
    {
        low = GetComponent<AudioLowPassFilter>();
        low.cutoffFrequency = 2000;
    }

}
