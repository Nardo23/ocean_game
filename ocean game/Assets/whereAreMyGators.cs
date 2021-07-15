using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whereAreMyGators : MonoBehaviour
{
    int gatorCount;
    public GameObject heart;
    AudioSource sor;
    public AudioClip heartNoise;
    public Transform player;

    public void gatorArrived()
    {
        gatorCount += 1;
        if(gatorCount > 1)
        {
            heart.SetActive(true);

            if(Vector2.Distance( new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < 14)
            {
                sor = GetComponent<AudioSource>();
                sor.PlayOneShot(heartNoise);
            }
            

        }
    }



}
