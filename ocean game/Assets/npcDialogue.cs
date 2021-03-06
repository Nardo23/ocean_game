using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    [SerializeField] private string dialogue = null;
    public bool mirrored;
    public Vector3 bubbleOffset;
    public Vector2 pitchRange= new Vector2(1f,1f);
    public float talkSpeed = .03f;
    public bool babyGator = false;
    public bool adultGator = false;
    public bool heart = false;
    
    
    // Start is called before the first frame update

    public string getSentences()
    {

        return dialogue;
    }
    public bool getMirrored()
    {
        return mirrored;
    }
    public Vector2 getOffset()
    {
        return bubbleOffset;
    }
    public Vector2 getPitchRange()
    {
        return pitchRange;
    }
    public float getTalkSpeed()
    {
        return talkSpeed;
    }
    public bool getBabyGator()
    {
        return babyGator;
    }
    public bool getAdultGator()
    {
        return adultGator;
    }
    public bool getHeart()
    {
        return heart;
    }
}
