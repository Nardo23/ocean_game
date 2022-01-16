using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapBlink : MonoBehaviour
{
    public Animator anim;
    public Player playerScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            if (playerScript.newSave == true)
            {
                anim.SetTrigger("blink");
            }
            
            Destroy(this.gameObject);
        }
    }
}
