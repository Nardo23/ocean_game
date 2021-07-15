using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    public GameObject heartObj;
    public campFire fireScript;
    AudioSource sor;
    public AudioClip heartnoise;

    // Start is called before the first frame update
    void Start()
    {
        sor = GetComponent<AudioSource>();
        heartObj.SetActive(true);
        fireScript.canToggle = false;
        sor.PlayOneShot(heartnoise);
    }
    
}
