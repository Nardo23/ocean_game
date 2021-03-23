using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    [SerializeField] private string dialogue = null;
    public bool mirrored;
    public Vector3 bubbleOffset;
    
    
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


}
