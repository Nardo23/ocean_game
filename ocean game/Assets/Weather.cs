using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public Player playerScript;
    public windShrine shrineScript;
    public rain rainScript;

    public bool day;
    public Animator lightAnim;
    
    public float dayLength = 10f;
    public float nightLength = 10f;
    public float timeRemaining;
    public float N, S, E, W;
    public float half, full;

    public float rainChance;
    bool changed = false;
    bool rainCheck = false;
    public float rainStartTime;
    bool rainBegan;
    bool waitingForRain = false;
    public Vector2 rainDurationRange;
    public float rainDuration;

    string Weth1 = "";
    string Weth = "";

    private AudioSource audSor;
    public AudioClip nightFallSound;

    dayNightToggle toggleScript;
    // Start is called before the first frame update
    void Start()
    {
        audSor = GetComponent<AudioSource>();

        dayLength *= 60;
        nightLength *= 60;
        if (day)
        {
            timeRemaining = dayLength;
        }
        else
        {
            timeRemaining = nightLength;
        }
        lightAnim.SetBool("day", day);

        toggleScript = GetComponent<dayNightToggle>();
        toggleScript.toggle(day);
    }

    void dayTick()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            day = !day;
            toggleScript.toggle(day);
            if (day)
            {
                timeRemaining = dayLength;
            }
            else
            {
                timeRemaining = nightLength;
            }
            
        }
        if (!day && nightLength - timeRemaining >= 10 && !changed)
        {
            audSor.PlayOneShot(nightFallSound);
            rainCheck = true;
            changed = true;
            if (!playerScript.shrineWindSet)
            {
                randomWindSpeed();
                randomWind();               
                //print("windDirection: "+playerScript.WindDirect + "speed: " + playerScript.windSpeed);
            }
            playerScript.shrineWindSet = false;
        }
        else if (day)
        {
            changed = false;
        }
        if(rainBegan || rainCheck || waitingForRain)
        {
            rainTick();
        }
        
        

    }

    void rainTick()
    {
        if (rainCheck)
        {
            float i = Random.Range(1f, 100f);
            if(rainChance >= i)
            {
                rainStartTime = Random.Range(1f, dayLength + nightLength - dayLength/2);
                waitingForRain = true;
            }
            else
            {
                rainBegan = false;
                rainCheck = false;
            }
        }
        else
        {
            if (rainStartTime> nightLength)
            {
                if (timeRemaining <= rainStartTime - nightLength && day)
                {
                    rainScript.raining = true;
                    rainBegan = true;
                    rainCheck = false;
                    waitingForRain = false;
                    rainDuration = Random.Range(rainDurationRange.x, rainDurationRange.x);                   
                }
                
            }
            else
            {
                if(timeRemaining >= rainStartTime && !rainBegan)
                {
                    rainScript.raining = true;
                    rainBegan = true;
                    rainCheck = false;
                    waitingForRain = false;
                    rainDuration = Random.Range(rainDurationRange.x, rainDurationRange.x);
                }
            }
            if (rainBegan)
            {
                rainDuration -= Time.deltaTime;
                if (rainDuration <= 0)
                {
                    rainScript.raining = false;
                    print("raining " + rainScript.raining);
                    rainBegan = false;

                }
            }
        }
    }



    void randomWind()
    {
        Weth1 = "";
        Weth = "";
        float i = Random.Range(1f, 100f);

        if(i <= N)
        {
            Debug.Log("pN");
            Weth = "North";
        }
        else if (i > N && i <= N + S)
        {
            Debug.Log("pS");
            Weth = "South";
        }

        i = Random.Range(1f, 100f);

        if (i <= E)
        {
            Debug.Log("pE");
            Weth1 = "East";
        }
        else if (i > E && i <= E + W)
        {
            Debug.Log("pW");
            Weth1 = "West";
        }
        Weth = Weth + Weth1;

        if (Weth == "")
        {
            randomWind();
        }
        Debug.Log(Weth);
        playerScript.setWindDirection(Weth);

    }

    void randomWindSpeed()
    {
        float i = Random.Range(1f, 100f);
        float speed = 0;
        if (i <= half)
        {
            speed = .5f;
            
        }
        else if (i > half && i <= half + full)
        {
            speed = 1;
        }
        else
        {

        }

        playerScript.windSpeed = speed;
    }



    // Update is called once per frame
    void Update()
    {
        dayTick();
        lightAnim.SetBool("day", day);

    }
}
