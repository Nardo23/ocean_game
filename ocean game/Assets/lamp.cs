using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lamp : MonoBehaviour
{
    GameObject weather;
    Weather weatherScript;
    public GameObject lightObj;

    // Start is called before the first frame update
    void Start()
    {
        weather = GameObject.Find("WeatherManager");
        weatherScript = weather.GetComponent<Weather>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weatherScript.day && lightObj.activeSelf)
        {
            lightObj.SetActive(false);
        }
        if(!weatherScript.day && !lightObj.activeSelf)
        {
            lightObj.SetActive(true);
        }
    }
}
