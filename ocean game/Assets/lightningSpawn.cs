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
    AudioSource sor;
    [SerializeField]
    AudioClip[] thunder;
    public Vector2 pitchRange;
    AudioReverbFilter reverb;
    AudioLowPassFilter lowpass;
    // Start is called before the first frame update
    void Start()
    {
        lowpass = GetComponent<AudioLowPassFilter>();
        reverb = GetComponent<AudioReverbFilter>();
        sor = GetComponent<AudioSource>();
        setGoal();
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        tick();
        if (playerScript.inUnderworld)
        {
            reverb.enabled = true;
            lowpass.enabled = true;
        }
        else
        {
            reverb.enabled = false;
            lowpass.enabled = false;
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
                    thunderSound();
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
        if (!playerScript.inUnderworld)
        {
            var newGameObject = Instantiate(lightning, transform.position, Quaternion.identity);
            newGameObject.transform.parent = null;
            newGameObject.transform.position = new Vector3(player.transform.position.x + Random.Range(positionRange.x, positionRange.y), player.transform.position.y + Random.Range(positionRange.x, positionRange.y), player.transform.position.z);
        }
        setGoal();
        SpawnedPrev = true;
        thunderSound();
        
    }

    void thunderSound()
    {
        sor.pitch = Random.Range(pitchRange.x, pitchRange.y);
        sor.PlayOneShot(thunder[Random.Range(0, thunder.Length)]);
    }


}
