using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantSounds : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] Smoosh;
    [SerializeField]
    public AudioClip[] Release;
    public Vector2 pitchRange;
    public float volume = 1;

    public float getVolume()
    {
        return volume;
    }
    public Vector2 getPitchRange()
    {
        return pitchRange;
    }

    public AudioClip[] getSmoosh()
    {
        return Smoosh;
    }


    public AudioClip[] getRelease()
    {
        return Release;
    }
  
}
