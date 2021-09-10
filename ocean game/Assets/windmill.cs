using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmill : MonoBehaviour
{
    public Player playerScript;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.windSpeed <= .2f)
        {
            anim.SetTrigger("stop");
            
        }
        else
        {
            anim.SetTrigger("go");
        }
    }
}
