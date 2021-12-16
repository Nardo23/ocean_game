using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluteTriggers : MonoBehaviour
{
    public fluteSong fluteScript;
    public float lowFreq = 800;
    public float vol = 1;
    public bool lowpass;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            fluteScript.inSide(vol, lowpass, lowFreq);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fluteScript.outSide();
        }
    }

}
