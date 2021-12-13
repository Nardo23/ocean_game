using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTrigger : MonoBehaviour
{
    AudioSource sor;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !triggered)
        {
            triggered = true;
            sor.Play();
        }
    }
}
