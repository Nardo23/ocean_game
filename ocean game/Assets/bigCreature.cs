using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigCreature : MonoBehaviour
{
    [SerializeField]
    Transform[] locations;
    Animator anim;
    Transform prevPosition;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        prevPosition = transform;
        newLocation();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !triggered)
        {
            triggered = true;
            //Debug.Log("whale");
            anim.SetTrigger("breach");
        }
    }

    public void newLocation()
    {
        //Debug.Log("sadfaeawf");
        int index = Random.Range(0, locations.Length-1);
        if (locations[index] == prevPosition)
        {
            if (index >= locations.Length)
            {
                index -= 1;
            }
            else
            {
                index += 1;
            }
        }
        transform.position = locations[index].position;
        prevPosition = locations[index];
        triggered = false;
    }



}
