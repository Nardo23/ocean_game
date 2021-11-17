using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wispManager : MonoBehaviour
{
    public GameObject wispS_N, wispN_S, wispE_W, wispW_E;
    public GameObject cloudsN, cloudsS, cloudsE, cloudsW;
    public Player playerScript;
    

    public void wispCheck(int age, int lastId)
    {
        if(age >= 5)
        {
            if(lastId == 1) //North
            {
                wispN_S.SetActive(true);
                cloudsS.GetComponent<endclouds>().ready = true; 

            }
            else if(lastId == 2) //East
            {
                wispE_W.SetActive(true);
                cloudsW.GetComponent<endclouds>().ready = true;
            }
            else if (lastId == 3) //South
            {
                wispS_N.SetActive(true);
                cloudsN.GetComponent<endclouds>().ready = true;
            }
            else if (lastId == 4) //West
            {
                wispW_E.SetActive(true);
                cloudsE.GetComponent<endclouds>().ready = true;
            }


        }



    }



}
