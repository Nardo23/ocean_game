using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionLayering : MonoBehaviour
{
    Animator anim;
    public GameObject boatReflect;
    public GameObject reflect;
    public float defaultY;
    public float verticalY;
    float state = 0;
    float prevState = 0;
    bool changed = false;
    Animator reflectionAnim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        reflectionAnim = reflect.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        reflectionToggle();
    }

    void reflectionToggle()
    {
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("pBoatD1") || anim.GetCurrentAnimatorStateInfo(0).IsName("pBoatU1"))
        {
            state = 1;
            gameObject.layer = 0;
            boatReflect.SetActive(true);

            if (reflect.activeSelf)
            {
                reflectionAnim.SetBool("horizontal", false);
            }
            

            if (state != prevState)
            {
                changed = false;
            }

            if (!changed)
            {
                
                changed = true;
            }


        }
        else
        {
            state = 2;
            gameObject.layer = 10;
            boatReflect.SetActive(false);

            if (reflect.activeSelf)
            {
                reflectionAnim.SetBool("horizontal", true);
            }

            if (state != prevState)
            {
                changed = false;
            }

            if (!changed)
            {
                
                changed = true;
            }
        }
        

        prevState = state;
    }


}
