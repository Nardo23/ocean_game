using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class magicMush : MonoBehaviour
{
    public AudioMixer mixer;
    private AudioMixerSnapshot normalSnap;
    private AudioMixerSnapshot tripSnap;
    public float reverbChangeTime = 2f;
    bool lower = false;
    bool raise = false;
    public float lowvalue;


    // Start is called before the first frame update
    void Start()
    {
        normalSnap = mixer.FindSnapshot("SnapshotDefault");
        tripSnap = mixer.FindSnapshot("Snapshot2");
    }

    private void Update()
    {
        float cur;
        mixer.GetFloat("TripPitch", out cur);

        if (raise)
        {
            if (Mathf.Abs(cur - 1) <= .01)
            {
                mixer.SetFloat("TripPitch", 1);
                raise = false;
            }
            else
            {
                raisePitch();
            }

        }
        

        if (lower)
        {
            if (Mathf.Abs(cur - lowvalue) <= .01)
            {
                mixer.SetFloat("TripPitch", lowvalue);
                lower = false;
            }
            else
            {
                lowerPitch();
            }
        }

    }

    private void OnDisable()
    {
        mixer.SetFloat("TripPitch", 1);
    }

    public void setHigh()
    {
        raise = false;
        lower = true;
    }


    void lowerPitch()
    {
        float cur;
        mixer.GetFloat("TripPitch", out cur);
        mixer.SetFloat("TripPitch", Mathf.MoveTowards(cur, lowvalue, 2 * Time.deltaTime));
    }

    void raisePitch()
    {
        float cur;
        mixer.GetFloat("TripPitch", out cur);
        mixer.SetFloat("TripPitch", Mathf.MoveTowards(cur, 1, 2 * Time.deltaTime));
    }

    public void tripSound()
    {
        tripSnap.TransitionTo(reverbChangeTime);
    }

    public void defaultSound()
    {
        normalSnap.TransitionTo(reverbChangeTime);
    }

}
