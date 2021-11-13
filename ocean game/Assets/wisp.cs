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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < detectRadius && activated == false)
        {
            activated = true;
            anim.SetTrigger("move");
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

    public void toggleLight()
    {
        wispLight.SetActive(!wispLight.activeSelf);
    }
   


}
