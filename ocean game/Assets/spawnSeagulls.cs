using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSeagulls : MonoBehaviour
{
    public GameObject seagull;
    public float timer = 0;
    public float spawnTimeMin;
    public float spawnTimeMax;
    float spawnTime;
    bool SpawnedPrev = false;
    public Player playerScript;
    float yStartVal;
    // Start is called before the first frame update
    void Start()
    {
        RandomSpawnTime();
        yStartVal = transform.position.y;
    }

    void RandomSpawnTime()
    {
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
    }

    void Spawn()
    { 
        Instantiate(seagull,transform);
        
        SpawnedPrev = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.inUnderworld)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0f;


                if (SpawnedPrev)
                {
                    if (Random.Range(1, 3) >= 2)
                    {
                        Spawn();
                    }
                    else
                    {
                        SpawnedPrev = false;
                    }
                }
                else
                {
                    Spawn();
                }


                RandomSpawnTime();

            }
        }
        

    }
}
