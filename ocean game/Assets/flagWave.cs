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
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        prevWind = "stopit";
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        prevWind = "stopit";
        anim.SetTrigger(playerScript.WindDirect);
        anim.SetFloat("windSpeed", playerScript.windSpeed);
    }



    // Update is called once per frame
    void Update()
    {
        if(!playerScript.inUnderworld && wasInUnderworld)
        {
            Debug.Log("wasinUNderworld");
            wasInUnderworld = false;
            
        }   
        
        if(playerScript.WindDirect != prevWind )
        {
            print("flag "+prevWind + "wind "+ playerScript.WindDirect);
            anim.SetTrigger(playerScript.WindDirect);
           
        }
        if(playerScript.windSpeed != prevSpeed)
        {
           // anim.SetTrigger(playerScript.WindDirect);
            anim.SetFloat("windSpeed", playerScript.windSpeed);
        }
        prevSpeed = playerScript.windSpeed;
        prevWind = playerScript.WindDirect;
        if (playerScript.inUnderworld)
        {
            wasInUnderworld = true;
            anim.ResetTrigger(playerScript.WindDirect);
            prevSpeed = 99;
            prevWind =("Underground");

        }
    }
}
