using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mast : MonoBehaviour
{
    public Player playerScript;
    public GameObject shrine;
    bool prevActivated = false;
    public GameObject mastObj;
    public GameObject smokeObj;
    bool startCount = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        smokeObj.SetActive(false);
    }

    void Update()
    {
        if (playerScript.DrownedShrine)
        {
            prevActivated = true;
        }
        if (prevActivated)
        {
            set();
        }
        if (startCount)
        {
            timer += Time.deltaTime;
            if(timer >= 4)
            {
                this.gameObject.SetActive(false);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("MAsttttt");
        if (collision.transform.tag == "Player" && shrine.activeSelf)
        {
           
            set();
            smokeObj.SetActive(true);
        }
        
        
    }



    void set()
    {
        mastObj.SetActive(false);
        startCount = true;
    }

}
