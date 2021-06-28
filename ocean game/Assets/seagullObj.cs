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
    float parallax = 35;
    // Start is called before the first frame update
    void Start()
    {
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
        speed = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.position =  new Vector3(transform.position.x- speed* Time.deltaTime -(bod.velocity.x/parallax), transform.position.y-(bod.velocity.y/ parallax), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x +speed * Time.deltaTime  -(bod.velocity.x / parallax), transform.position.y - (bod.velocity.y / parallax), transform.position.z);
            
        }

       // Debug.Log(bod.velocity);


    }
}
