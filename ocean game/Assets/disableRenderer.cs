using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableRenderer : MonoBehaviour
{
    public Player playerScript;
    SpriteRenderer render;
    public bool wasInUnderworld = false;
    

    void disableRend()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            render = GetComponent<SpriteRenderer>();
            render.enabled = false;
        }

        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
            

        }

    }

    void enableRend()
    {
        if(GetComponent<SpriteRenderer>() != null)
        {
            render = GetComponent<SpriteRenderer>();
            render.enabled = true;
        }
        
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }

        }
    }



    // Update is called once per frame
    void Update()
    {
        if(playerScript.inUnderworld == true && wasInUnderworld == false)
        {
            disableRend();
        }
        else if(playerScript.inUnderworld == false && wasInUnderworld == true)
        {
            enableRend();
        }

        wasInUnderworld = playerScript.inUnderworld;
    }
}
