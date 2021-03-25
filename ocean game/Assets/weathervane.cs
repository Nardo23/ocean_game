using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weathervane : MonoBehaviour
{
    Animator vaneAnim;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        vaneAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        vaneAnim.SetTrigger(playerScript.WindDirect);
    }
}
