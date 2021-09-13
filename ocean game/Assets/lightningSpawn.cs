using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningSpawn : MonoBehaviour
{
    public GameObject lightning;
    float timer = 0;
    public Vector2 timeRange;
    float goal;
    bool SpawnedPrev = false;
    public Vector2 positionRange;
    public GameObject player;
    Player playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        setGoal();
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.inUnderworld)
        {
            tick();
        }

    }

    void tick()
    {
        timer += Time.deltaTime;

        if (timer >= goal)
        {
            //spawn lightning
            if (SpawnedPrev)
            {
                if (Random.Range(1, 3) >= 2)
                {
                    spawn();
                }
                else
                {
                    SpawnedPrev = false;
                }
            }
            else
            {
                spawn();
            }
        }
    }


    void setGoal()
    {
        goal = Random.Range(timeRange.x, timeRange.y);
        timer = 0;
    }

    public void spawn()
    {
        setGoal();
        SpawnedPrev = true;
       
        var newGameObject = Instantiate(lightning, transform.position, Quaternion.identity);
        newGameObject.transform.parent = null;
        newGameObject.transform.position = new Vector3(player.transform.position.x + Random.Range(positionRange.x, positionRange.y), player.transform.position.y + Random.Range(positionRange.x, positionRange.y), player.transform.position.z);
    }

}
