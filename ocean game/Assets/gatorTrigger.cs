using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatorTrigger : MonoBehaviour
{

    bool activated = false;
    public alligator gator1;
    public alligator gator2;
    public GameObject barrier;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            if (!activated)
            {
                if (gator1.following == false && !gator1.arrived)
                {
                    Debug.Log("gatorGate");
                    barrier.SetActive(false);
                    activated = true;
                    gator1.following = true;
                    gator2.following = true;
                }
            }
            
        }
    }



}
