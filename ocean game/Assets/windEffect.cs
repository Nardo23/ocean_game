using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windEffect : MonoBehaviour
{
    public Player playerScript;
    public GameObject medWind;
    public GameObject highWind;

    
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void WindChange()
    {
        anim.SetTrigger(playerScript.WindDirect);
        

        if (!playerScript.inUnderworld)
        {
            Debug.Log("Pee");
            if (playerScript.windSpeed > .5f)
            {
                highWind.SetActive(true);
                medWind.SetActive(false);
            }
            else if (playerScript.windSpeed == 0)
            {
                highWind.SetActive(false);
                medWind.SetActive(false);
            }
            else
            {
                highWind.SetActive(false);
                medWind.SetActive(true);
            }
        }
        else
        {
            highWind.SetActive(false);
            medWind.SetActive(false);
        }
        

    }


  
}
