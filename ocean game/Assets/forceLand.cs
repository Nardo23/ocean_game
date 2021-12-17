using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceLand : MonoBehaviour
{
    public Player playerScript;

    bool inTrig = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("arghhhh");
            inTrig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("woof");
            inTrig = false;
        }
    }

    private void Update()
    {
        if(inTrig)
            playerScript.triggerLandCount = 2;
    }



}
