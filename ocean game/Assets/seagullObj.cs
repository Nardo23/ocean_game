using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seagullObj : MonoBehaviour
{
    float timer = 0;
    float speed;
    Animator anim;
    GameObject player;
    Rigidbody2D bod;
    float parallaxX = 22;
    float parallaxY = 32;
    [SerializeField]
    AudioClip[] clips;
    AudioSource sor;
    bool played = false;
    float clipTime;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
        if (Random.Range(1, 10) >= 5)
        {
            transform.localScale = new Vector3 (-1,1,1);
            transform.position = new Vector3(transform.position.x - 26, transform.position.y, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-5, 5), transform.position.z);
        player = GameObject.FindGameObjectWithTag("Player");
        bod = player.GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.Play(0, -1, Random.value);
        speed = Random.Range(7, 10);

        clipTime = Random.Range(1, 5);


    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().inUnderworld)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        if (timer > 11f )
        {
            if (transform.position.x > player.transform.position.x + 13 || transform.position.x < player.transform.position.x - 13)
            {
                Destroy(gameObject);
            }
            
        }
        if(transform.localScale.x == 1)
        {
            transform.position =  new Vector3(transform.position.x- speed* Time.deltaTime -(bod.velocity.x/parallaxX), transform.position.y-(bod.velocity.y/ parallaxY), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x +speed * Time.deltaTime  -(bod.velocity.x / parallaxX), transform.position.y - (bod.velocity.y / parallaxY), transform.position.z);
            
        }

        if (clipTime > clipTime-.8f && clipTime < clipTime + .2f && !played)
        {
            sor.pitch = Random.Range(.85f, 1.15f);
            sor.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            played = true;
        }
        if (clipTime>= clipTime + .2f)
        {
            played = false;
            clipTime += Random.Range(1.5f, 3.5f);
        }

       // Debug.Log(bod.velocity);





    }
}
