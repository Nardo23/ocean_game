using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class windAudio : MonoBehaviour
{
    public float againstWindLow;
    public float againstWindHigh;
    public float againstWindMain;
    public float withWindLow;
    public float withWindHigh;
    public float withWindMain;
    public float notmoveMain;

    float mainTarget;
    float highTarget;
    float lowTarget;

    public float speed;

    public Weather weatherScript;
    public Player playerScript;

    float dot;
    float prevWindSpeed = -1;
    Animator anim;
    AudioSource sor;
    public AudioClip sailClip;

    float prevDot;
    bool prevMove;
    
    public AudioMixer mixer;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sor = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        windDot();
        mixAdjust();
        windSpeedAdjust();

    }

    void windSpeedAdjust()
    {
        if(playerScript.windSpeed != prevWindSpeed)
        {
            if (playerScript.windSpeed == 0)
            {
                anim.SetTrigger("none");
            }
            else if (playerScript.windSpeed > 0 && playerScript.windSpeed < 1)
            {
                anim.SetTrigger("half");
            }
            else if (playerScript.windSpeed >= 1)
            {
                anim.SetTrigger("full");
            }
        }
        prevWindSpeed = playerScript.windSpeed;
    }


    void mixAdjust()
    {
        float cur;
        mixer.GetFloat("WindMainVol", out cur);
        mixer.SetFloat("WindMainVol", Mathf.MoveTowards(cur, mainTarget, speed * Time.deltaTime));

        mixer.GetFloat("WindHighVol", out cur);
        mixer.SetFloat("WindHighVol", Mathf.MoveTowards(cur, highTarget, speed * Time.deltaTime));

        mixer.GetFloat("WindLowVol", out cur);
        mixer.SetFloat("WindLowVol", Mathf.MoveTowards(cur, lowTarget, speed * Time.deltaTime));
    }



    void windDot()
    {
        dot = Vector2.Dot(playerScript.playerDirection.normalized, playerScript.windDirection.normalized);

        if (dot == 0)
        {
            mainTarget = -3;
            highTarget = 0;
            lowTarget = 0;
        }
        else if (dot > 0)
        {
            mainTarget = withWindMain* Mathf.Abs(dot);
            highTarget = withWindHigh* Mathf.Abs(dot);
            lowTarget = withWindLow* Mathf.Abs(dot);
            
        }
        else if (dot < 0)
        {
            mainTarget = againstWindMain * Mathf.Abs(dot);
            highTarget = againstWindHigh * Mathf.Abs(dot);
            lowTarget = againstWindLow * Mathf.Abs(dot);
        }

        if (playerScript.inUnderworld)
        {
            mainTarget -= 5; 
        }
        if (!playerScript.moving || playerScript.onLand)
        {
            mainTarget = notmoveMain;

        }
        if(!playerScript.onLand&& prevMove)
        {
            sor.pitch = Random.Range(.82f, 1.18f);
            sor.PlayOneShot(sailClip);

        }

        
            prevDot = dot;

        prevMove = playerScript.onLand;
    }


}
