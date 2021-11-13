﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endclouds : MonoBehaviour
{
    public ParticleSystem particles;
    bool activated = false;
    float timer =0;
    public EdgeCollider2D worldBounds;
    public GameObject endArea;
    public GameObject player;
    bool positionSet = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        var emitParams = new ParticleSystem.EmitParams();
        particles.Emit(emitParams, 30);
        particles.Pause();
        
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (activated)
        {
            if(timer >= 1.2f && !positionSet)
            {
                positionSet = true;
                player.transform.position = new Vector3(endArea.transform.position.x, endArea.transform.position.y, 0f);
                this.enabled = false;

            }
            else
            {
                timer += Time.deltaTime;
            }
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" &&!activated)
        {
            particles.Play();
            activated = true;
            worldBounds.enabled = false;
            endArea.SetActive(true);
        }
    }


}
