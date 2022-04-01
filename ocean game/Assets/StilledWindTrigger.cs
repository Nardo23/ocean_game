using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StilledWindTrigger : MonoBehaviour
{
    public bool cave, boat;
    public stilledWind mainScript;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (boat)
            {
                mainScript.boatSor();
                //Debug.Log("boat");
            }
            if (cave)
            {
                mainScript.caveSor();
                //Debug.Log("cave");
            }

        }
    }

   

}
