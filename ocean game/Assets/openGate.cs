using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGate : MonoBehaviour
{
    public GameObject gate;
    public GameObject cam;
    public GameObject player;
    Player playerScript;
    Animator leverAnim, gateAnim;
    public float lookTime;

    public bool enableOrDisable = true;
    public bool worldChange = false;

    bool revealed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        gateAnim = gate.GetComponent<Animator>();
        leverAnim = GetComponent<Animator>();

    }

    public void setCamPos()
    {
        cam.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y, cam.transform.position.z);
        if (worldChange)
        {
            playerScript.SwapWorld();
        }
        gateAnim.SetTrigger("lift");
    }

    public void resetCamPos()
    {
        transform.localScale = new Vector3(-1f, 1, 1);
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cam.transform.position.z);
        playerScript.canMove = true;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !revealed)
        {
            revealed = true;
            playerScript.canMove = false;
            
           
            
            leverAnim.SetTrigger("on");


        }
    }

}

