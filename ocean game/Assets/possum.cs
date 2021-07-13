using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class possum : MonoBehaviour
{
    Animator animP;
    public Animator animF;
    bool canNext = true;

    // Start is called before the first frame update
    void Start()
    {
        animP = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void sleep()
    {
        animF.SetTrigger("sleep");
    }
    public void idle()
    {
        animF.SetTrigger("idle");
    }
    public void walk1()
    {
        animF.SetTrigger("walk1");
    }
    public void walk2()
    {
        animF.SetTrigger("walk2");
    }
    public void swim()
    {
        animF.SetTrigger("swim");
    }
    public void dig()
    {
        animF.SetTrigger("dig");
    }
    public void spin()
    {
        animF.SetTrigger("spin");
    }

    public void reset()
    {
        canNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" &&canNext)
        {
            canNext = false;
            animP.SetTrigger("next");
        }
        
    }

}
