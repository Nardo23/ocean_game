using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windShrine : MonoBehaviour
{
    Player playerScript;
    Animator capstanAnim;
   
   
    // Start is called before the first frame update
    void Start()
    {
        capstanAnim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
       



    }

    string nextWind(string wind)
    {
        if(wind == "North")
        {
            wind = "NorthEast";
        }
        else if (wind == "NorthEast")
        {
            wind = "East";
        }
        else if (wind == "East")
        {
            wind = "SouthEast";
        }
        else if (wind == "SouthEast")
        {
            wind = "South";
        }
        else if (wind == "South")
        {
            wind = "SouthWest";
        }
        else if (wind == "SouthWest")
        {
            wind = "West";
        }
        else if (wind == "West")
        {
            wind = "NorthWest";
        }
        else if (wind == "NorthWest")
        {
            wind = "North";
        }
        return wind;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Player>() != null)
            {
                playerScript = collision.GetComponent<Player>();
                string winds = nextWind(playerScript.WindDirect);
                capstanAnim.SetTrigger("rotate");
                //capstanAnim.ResetTrigger("rotate");
                playerScript.WindDirect = winds;
                playerScript.windDirection =playerScript.setWindDirection(winds);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            capstanAnim.ResetTrigger("rotate");
            
        }
    }



}
