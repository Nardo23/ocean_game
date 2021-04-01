using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagWave : MonoBehaviour
{
    Animator anim;
    public Player playerScript;
    private string prevWind;
    private float prevSpeed = -1f;
    private bool wasInactive;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        prevWind = "stopit";
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (gameObject.activeSelf == true && wasInactive)
        {
            
            anim.SetTrigger(playerScript.WindDirect);
            anim.SetFloat("windSpeed", playerScript.windSpeed);
        }
        */
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
        wasInactive = !gameObject.activeSelf;
    }
}
