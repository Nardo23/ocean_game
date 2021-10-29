using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionSound : MonoBehaviour
{
    public AudioSource sor;
    [SerializeField]
    AudioClip[] clips;

    public float pitchMin = .9f, pitchMax = 1.1f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sor.pitch = Random.Range(pitchMin, pitchMax);
            sor.PlayOneShot(clips[Random.Range(0, clips.Length)]);

        }
    }
}
