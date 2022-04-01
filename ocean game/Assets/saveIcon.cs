using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveIcon : MonoBehaviour
{
    public Animator anim;
    

    public void saveAnim()
    {
        anim.Play("Base Layer.save");
    }

}
