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
        GetComponent<BoxCollider2D>().enabled = false;
        playerScript.SwapWorld();
        openGateScript.resetCamPos();
        gameObject.SetActive(false);

    }

    void Update()
    {
        if (playerScript.WinterShrine == true)
        {
            openGateScript.lockLever();
            this.gameObject.SetActive(false);
        }
    }



}
