using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campFire : MonoBehaviour
{
    public GameObject fire;
    public bool canToggle = true;
    public AudioClip fireStartSound;
    AudioSource sor;

    private void Start()
    {
        sor = GetComponent<AudioSource>();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (canToggle)
            {
                fire.SetActive(!fire.activeSelf);

                if (fire.activeSelf == true)
                {
                    sor.PlayOneShot(fireStartSound);

                }
            }
             
        }
    }

}
