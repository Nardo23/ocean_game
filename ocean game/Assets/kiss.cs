using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kiss : MonoBehaviour
{
    public GameObject fire;
    [SerializeField]
    AudioClip[] kissClip;
    AudioSource sor;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && fire.activeSelf)
        {
            sor.pitch = Random.Range(.85f, 1.15f);
            sor.PlayOneShot(kissClip[Random.Range(0, kissClip.Length)]);
            //gameObject.tag = "Untagged";
        }
    }
}
