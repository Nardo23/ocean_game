using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windShaders : MonoBehaviour
{

    public Material[] windMats;
    public GameObject[] pflags;
    
    bool started = false;
    public void Start()
    {
        if (!started)
        {
            startStuff();
            started = true;
        }

        


    }

    void startStuff()
    {
        pflags = GameObject.FindGameObjectsWithTag("pflag");
        
    }


    public void changeWinds(Vector2 direction, float speed)
    {
        if (!started)
        {
            startStuff();
            started = true;
        }
        foreach (var material in windMats)
        {
            


            if (speed > .5)
            {
                material.SetVector("_Direct", direction*2f);
            }
            else if (speed > 0)
            {
                material.SetVector("_Direct", direction * 1.1f);
            }
            else
            {
                material.SetVector("_Direct", direction * .5f);
            }
            


        }
        
        foreach(GameObject obj in pflags)
        {
            //Debug.Log("pflgas");
            if (obj.GetComponent<Animator>() != null)
            {
                
                if(direction.x >= 0)
                {
                    obj.transform.localScale = new Vector3(-1, 1, 1);   
                }
                if (direction.x < 0)
                {
                    obj.transform.localScale = new Vector3(1, 1, 1);
                }
                if (speed > .5)
                {
                    obj.GetComponent<Animator>().Play("Base Layer.pflag fast", 0, Random.Range(0f,.2f));
                }
                else if (speed > 0)
                {
                    obj.GetComponent<Animator>().Play("Base Layer.pflagslow", 0, Random.Range(0f, .2f));
                }
                else
                {
                    obj.GetComponent<Animator>().Play("Base Layer.pflagidle", 0, Random.Range(0f, .2f));
                }
            }

            
        }




    }


}
