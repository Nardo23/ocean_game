using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public Player playerScript;
    public windShrine shrineScript;

    public bool day;
    public Animator lightAnim;
    public float dayLength = 10f;
    public float nightLength = 10f;
    public float timeRemaining;
    public float N, S, E, W;
    public float half, full;
    bool changed = false;

    string Weth1 = "";
    string Weth = "";
    // Start is called before the first frame update
    void Start()
    {
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
    }

    void dayTick()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            day = !day;
            if (day)
            {
                timeRemaining = dayLength;
            }
            else
            {
                timeRemaining = nightLength;
            }
            
        }
        if (!day && nightLength - timeRemaining >= 1 && !changed)
        {
            changed = true;
            if (!playerScript.shrineWindSet)
            {
                randomWindSpeed();
                randomWind();
                
                print("windDirection: "+playerScript.WindDirect + "speed: " + playerScript.windSpeed);
            }
        }
        else if (day)
        {
            changed = false;
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

        playerScript.windSpeed = speed;
    }



    // Update is called once per frame
    void Update()
    {
        dayTick();
        lightAnim.SetBool("day", day);



    }
}
