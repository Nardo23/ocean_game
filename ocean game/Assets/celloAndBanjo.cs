using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celloAndBanjo : MonoBehaviour
{
    public AudioSource cello;
    public AudioSource banjo;
    public Transform player;

    float maxStore;
    bool started = false;
    float timer = 0;
    
    void Start()
    {
        maxStore = banjo.maxDistance;
        


    }

    private void Awake()
    {
        
    }


    void Update()
    {
        if(timer < 4 && !started)
        {
            timer += Time.deltaTime;
        }
        else if (timer >=4 &&!started)
        {
            started = true;
            cello.Play();
            banjo.Play();
            timer = 0;
        }

        if (started)
        {
            cello.timeSamples = banjo.timeSamples;
        }



       if(Vector2.Distance (player.transform.position, cello.gameObject.transform.position) < cello.maxDistance)
        {
            banjo.maxDistance = maxStore + Vector2.Distance(player.transform.position, banjo.gameObject.transform.position) *.75f;
        }
        else
        {
            if(banjo.maxDistance> maxStore)
            {
                banjo.maxDistance = banjo.maxDistance - 2 * Time.deltaTime;
            }
            else
            {
                banjo.maxDistance = maxStore;
            }
            
        }



    }
}
