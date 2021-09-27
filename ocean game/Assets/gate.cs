using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public openGate openGateScript;
    public Player playerScript;
    // Start is called before the first frame update
    AudioSource sor;
    public AudioClip gateClip;
    public bool DrownedShrine = false;
    public bool startloaded = false;
    private void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    public void gateSound()
    {
        sor.PlayOneShot(gateClip);
    }

    public void opened()
    {
        if (!DrownedShrine)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            playerScript.SwapWorld();
            openGateScript.resetCamPos();
            gameObject.SetActive(false);
        }
        else
        {
            
            if(playerScript.inUnderworld && startloaded)
                playerScript.SwapWorld();
            openGateScript.resetCamPos();
        }
        

    }

    void Update()
    {
        if (DrownedShrine)
        {
            if (playerScript.DrownedShrine == true)
            {
                startloaded = true;
                GetComponent<Animator>().SetTrigger("lift");
                openGateScript.lockLever();
            }
        }
        else
        {
            if (playerScript.WinterShrine == true)
            {
                openGateScript.lockLever();
                this.gameObject.SetActive(false);
            }
        }
        
    }



}
