using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whereAreMyGators : MonoBehaviour
{
    int gatorCount;
    public GameObject heart;

    public void gatorArrived()
    {
        gatorCount += 1;
        if(gatorCount > 1)
        {
            heart.SetActive(true);
        }
    }



}
