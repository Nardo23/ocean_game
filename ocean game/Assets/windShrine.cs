using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windShrine : MonoBehaviour
{
    Player playerScript;
    public Weather weatherScript;
    Animator capstanAnim;
    AudioSource sor;
    public Vector2 pitchRange;
    public AudioClip windClip;
    [Header("1=North, 2=East, 3=South, 4=West")]
    [Range(1, 4)]
    public int Id;
    public GameObject fire;
    float timeS;
    bool inShrine = false;


    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();

        capstanAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inShrine)
        {
            timeS += 1 * Time.deltaTime;
        }
        
       
    }

    string nextWind(string wind)
    {
        if (wind == "North")
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


    void turnWind()
    {
        string winds = nextWind(playerScript.WindDirect);
        capstanAnim.SetTrigger("rotate");
        //capstanAnim.ResetTrigger("rotate");
        if (playerScript.windSpeed == 0)
        {
            playerScript.windSpeed = .5f;
        }
        if (playerScript.windSpeed != 0)
        {
            sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            sor.PlayOneShot(windClip);
        }

        playerScript.WindDirect = winds;
        playerScript.windDirection = playerScript.setWindDirection(winds);

        if (weatherScript.day && weatherScript.timeRemaining / 4 <= weatherScript.dayLength)
        {
            playerScript.shrineWindSet = true;
        }

        playerScript.IncreaseAge(Id);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Player>() != null)
            {
                inShrine = true;
                playerScript = collision.GetComponent<Player>();
                fire.SetActive(true);
                turnWind();
            }

        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Player>() != null)
            {
                playerScript = collision.GetComponent<Player>();
                if(timeS >= 2.5 && Input.GetAxisRaw("Horizontal") != 0 )
                {
                    turnWind();
                    timeS = 0;
                }
                else if (timeS >= 2.5 && Input.GetAxisRaw("Vertical") != 0)
                {
                    turnWind();
                    timeS = 0;
                }

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inShrine = false;
            timeS = 0;
            capstanAnim.ResetTrigger("rotate");
            
        }



    }
}
