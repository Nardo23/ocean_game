using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squid : MonoBehaviour
{
    [SerializeField]
    Animator[] tentacle;
    int count =0;
    // Start is called before the first frame update
    void Start()
    {
        tentacleOff();
    }

  
    void breach()
    {
        tentacle[count].gameObject.SetActive(true);
        count++;
    }
    void resetCount()
    {
        count = 0;
    }

    void retract()
    {
        tentacle[count].SetTrigger("retract");
        count++;
    }

    void tentacleOff()
    {
        foreach (Animator a in tentacle)
        {
            a.gameObject.SetActive(false);
        }
    }


}
