using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    AudioSource sor;
    [SerializeField]
    AudioClip[] coinSound;
    public float pitchMin = .9f, pitchMax = 1.1f;
    public float soundTime = .5f;
    float goalTime;
    float timer;
    GameObject player;
    Transform pTrans;
    Vector3 prevTrans;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pTrans = player.GetComponent<Transform>();
        sor = GetComponent<AudioSource>();
        prevTrans = transform.position;
        goalTime = 0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector3.Distance(pTrans.position, transform.position) >25)
        {
            sor.volume = 0;
        }
        else
        {
            sor.volume = 1;
        }


        if (Vector3.Distance(transform.position, prevTrans) >= .0125f)
        {
            timer += Time.deltaTime;
            if (timer >= goalTime)
            {
                soundPlay();
                timer = 0f;
                setGoal();
            }
        }
        else
        {
            timer = 0f;
        }
        prevTrans = transform.position;
    }
    
    
    void soundPlay()
    {
        sor.pitch = Random.Range(pitchMin, pitchMax);
        sor.PlayOneShot(coinSound[Random.Range(0, coinSound.Length)]);
    }

    void setGoal()
    {
        goalTime = soundTime + (Random.Range(-.1f, .1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            soundPlay();
        }
    }

}
