using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagWave : MonoBehaviour
{
    Animator anim;
    public Player playerScript;
    private string prevWind;
    private float prevSpeed = -1f;
    private bool wasInUnderworld;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
    
        anim = GetComponent<Animator>();
        prevWind = "stopit";
        started = true;

    }

    private void OnEnable()
    {
        if (started)
        {
            anim = GetComponent<Animator>();
            prevWind = "stopit";
            anim.SetTrigger(playerScript.WindDirect);
            anim.SetFloat("windSpeed", playerScript.windSpeed);
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        
        
        if(playerScript.WindDirect != prevWind )
        {
            //print("flag "+prevWind + " wind "+ playerScript.WindDirect);
            anim.SetTrigger(playerScript.WindDirect);
           
        }
        if(playerScript.windSpeed != prevSpeed)
        {
           // anim.SetTrigger(playerScript.WindDirect);
            anim.SetFloat("windSpeed", playerScript.windSpeed);
        }
        prevSpeed = playerScript.windSpeed;
        prevWind = playerScript.WindDirect;
       
    }
}
