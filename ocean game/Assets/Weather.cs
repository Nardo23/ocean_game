using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public bool day;
    public Animator lightAnim;
    public float dayLength = 10f;
    public float nightLength = 10f;
    public float timeRemaining;
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
    }


    // Update is called once per frame
    void Update()
    {
        dayTick();
        lightAnim.SetBool("day", day);

    }
}
