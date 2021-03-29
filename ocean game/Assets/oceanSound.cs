using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oceanSound : MonoBehaviour
{

    GameObject[] LandAudios;
    public GameObject player;
    int closestAudio;
    float prev;
    private AudioSource sor;
    public float vol = .5f;
    public float minvol = 0f;
    public float maxDistance;
    public float minDistance;

    AudioSource LandSource;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
        LandAudios = GameObject.FindGameObjectsWithTag("LandAudio");
        prev = Vector3.Distance(player.transform.position, LandAudios[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < LandAudios.Length; i++)
        {
            float dist = Vector3.Distance(player.transform.position, LandAudios[i].transform.position);


            if (dist < prev)
            {
                prev = dist;
                closestAudio = i;


            }



        }
        LandSource = LandAudios[closestAudio].GetComponent<AudioSource>();
        maxDistance = LandSource.maxDistance;
        minDistance = LandSource.minDistance;


        //Debug.Log(prev);
        if(prev> maxDistance)
        {
            sor.volume = vol;
        }
        else if (prev< minDistance)
        {
            sor.volume = minvol;
        }
        else
        {
            sor.volume = prev / maxDistance * vol;
            
        }
        

        prev = Vector3.Distance(player.transform.position, LandAudios[0].transform.position);


    }

    




}
