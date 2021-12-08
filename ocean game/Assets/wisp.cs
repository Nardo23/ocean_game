using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wisp : MonoBehaviour
{
    public bool up, down, left, right;
    public Animator anim;
    public GameObject player;
    public float detectRadius, respawnRadius;
    bool activated = false;
    public GameObject wispLight;
    AudioSource sor;
    public AudioSource stickSource;
    [SerializeField]
    AudioClip[] WispClips;
    [SerializeField]
    AudioClip[] StickClips;
    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < detectRadius && activated == false)
        {
            activated = true;
            anim.SetTrigger("move");
            soundPlay();
            //toggleLight();
            if (up)
                transform.eulerAngles = new Vector3 (0,0,180);
            if (down)
                transform.eulerAngles = Vector3.zero;
            if(left)
                transform.eulerAngles = new Vector3(0, 0, -90);
            if(right)
                transform.eulerAngles = new Vector3(0, 0, 90);

        }

        if (activated && Vector2.Distance(player.transform.position, transform.position)> respawnRadius)
        {
            activated = false;
            transform.eulerAngles = Vector3.zero;
            anim.SetTrigger("idle");
            //toggleLight();
        }


    }

    private void soundPlay()
    {
        sor.PlayOneShot(WispClips[Random.Range(0, WispClips.Length)]);
        if (Random.Range(1, 10) >= 3)
        {
            stickSource.pitch = Random.Range(.8f, 1.2f);
            stickSource.PlayOneShot(StickClips[Random.Range(0, StickClips.Length)]);
        }
    }


    public void toggleLight()
    {
        wispLight.SetActive(!wispLight.activeSelf);
    }
   





}
