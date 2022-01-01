using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceSlide : MonoBehaviour
{
    public GameObject player;
    Player playerScript;
    Animator anim;
    public GameObject bottom;
    Collider2D col;
    AudioSource sor;
    public AudioSource sor2;
    public AudioClip slide;
    public AudioClip splash;
    bool shift= false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        sor = GetComponent<AudioSource>();
        sor.volume = 0;
        sor2.volume = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.transform.parent = this.gameObject.transform;
            anim.SetTrigger("slide");
            playerScript.canMove = false;
            bottom.SetActive(false);
            col.enabled = false;
            sor.volume = 1;
            sor2.volume =.9f;
            sor.PlayOneShot(slide);
            shift = true;
        }
    }

    private void Update()
    {
        if(shift && sor.pitch> .4f)
        {
            sor.pitch -= sor.pitch * Time.deltaTime * 1;
        }
    }

    void Splash()
    {
        
        sor2.PlayOneShot(splash);
    }
    void endSlide()
    {
        player.transform.parent = null;
        playerScript.canMove = true;
        bottom.SetActive(true);
        shift = false;
        sor.pitch = 1.3f;
        
    }

    void EnableCOl()
    {
        col.enabled = true;
    }


    






}
