using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windShaders : MonoBehaviour
{

    public Material[] windMats;
    


  



    public void changeWinds(Vector2 direction, float speed)
    {

        foreach(var material in windMats)
        {
            


            if (speed > .5)
            {
                material.SetVector("_Direct", direction*2f);
            }
            else if (speed > 0)
            {
                material.SetVector("_Direct", direction * 1.1f);
            }
            else
            {
                material.SetVector("_Direct", direction * .5f);
            }
            


        }

    }


}
