﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapBlink : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetTrigger("blink");
            Destroy(this.gameObject);
        }
    }
}
