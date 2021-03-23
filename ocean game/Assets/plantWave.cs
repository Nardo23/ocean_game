using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class plantWave : MonoBehaviour
{
    public GameObject waterMask;
    public ParticleSystem Ripples;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "caveWater")
        {
            waterMask.SetActive(true);
            Ripples.Play();
        }
        if (collision.gameObject.tag == "plant")
        {
          
            GameObject plant = collision.gameObject;
            Animator anim = plant.GetComponent<Animator>();

            anim.SetTrigger("wave");
            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "caveWater")
        {
            waterMask.SetActive(false);
            Ripples.Stop();
        }
        if (collision.gameObject.tag == "plant")
        {
          
            GameObject plant = collision.gameObject;
            Animator anim = plant.GetComponent<Animator>();

            anim.SetTrigger("reset");
            
        }
    }

}
