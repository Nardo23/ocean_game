using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endArea : MonoBehaviour
{
    public GameObject player;
    float timer =0;
    public GameObject reflectStuff;
    public Weather weatherScript;
    public rain rainScript;
    Player playerScript;
    waterColor waterColorScript;
    public Animator Sun;
    public float weatherEnd, reflectionTime = 30, sunsetStart;
    bool weatherEnded = false, sunsetStarted = false;
    bool reflect = false;
    public Animator overworldAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        weatherScript.enabled = false;
        waterColorScript = GetComponent<waterColor>();
    }

    // Update is called once per frame
    void Update()
    {
        timeTick();



    }

    void timeTick()
    {
        if (timer < 60 * 10)// stop increasing timer after 10 minutes just in case 
        {
            timer += Time.deltaTime;

            if (timer >= weatherEnd && !weatherEnded)
            {
                weatherEnded = true;
                endWeather();
            }
            else if (timer >= sunsetStart && !sunsetStarted)
            {
                sunsetStarted = true;
                sunset();
            }
            else if (timer >= reflectionTime && reflect == false)
            {
                reflectStuff.SetActive(true);
                reflect = true;
            }
            

        }

    }

    void endWeather()
    {
        playerScript.windSpeed = 0;
        playerScript.setWindDirection(playerScript.WindDirect);
        rainScript.raining = false;

    }

    void sunset()
    {
        Sun.SetTrigger("endLight");
        overworldAnim.SetBool("day", true);
        waterColorScript.setColorFromScript();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }


}
