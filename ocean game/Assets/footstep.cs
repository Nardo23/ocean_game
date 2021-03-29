using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstep : MonoBehaviour
{
    public Player playerScript;
    [SerializeField]
    AudioClip[] SandSteps;
    AudioSource Sor;
    AudioReverbFilter reverb;
    public Vector2 pitchRange;

    // Start is called before the first frame update
    void Start()
    {
        Sor = GetComponent<AudioSource>();
        reverb = GetComponent<AudioReverbFilter>();
    }

    void Step()
    {
        if (playerScript.inUnderworld)
        {
            reverb.enabled = true;
        }
        else
        {
            reverb.enabled = false;
        }


        if (playerScript.sfxTerrainType == 1)
        {
            Sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            Sor.PlayOneShot(SandSteps[UnityEngine.Random.Range(0, SandSteps.Length)]);

        }
    }




    // Update is called once per frame
    void Update()
    {
        



    }
}
