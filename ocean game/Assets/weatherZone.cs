using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherZone : MonoBehaviour
{
    public float N, S, E, W;
    public float half, full;

    public float rainChance;
    public bool snow;

    public Weather weatherScript;
    public Player playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            weatherScript.N = N;
            weatherScript.S = S;
            weatherScript.E = E;
            weatherScript.W = W;
            weatherScript.half = half;
            weatherScript.full = full;
            weatherScript.rainChance = rainChance;
            playerScript.snow = snow;
        }
    }



}
