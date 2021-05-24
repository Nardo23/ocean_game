using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public openGate openGateScript;
    public Player playerScript;
    // Start is called before the first frame update
    

    public void opened()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        playerScript.SwapWorld();
        openGateScript.resetCamPos();


    }


}
