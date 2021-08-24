using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagAdjust : MonoBehaviour
{
    ysort ysortScript;
    public Player playerScript;
    bool wasSouth = false;
    float offStore;
    // Start is called before the first frame update
    void Start()
    {
        ysortScript = GetComponent<ysort>();
        offStore = ysortScript.offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.WindDirect == "South"|| playerScript.WindDirect == "SouthEast"|| playerScript.WindDirect == "SouthWest")
        {
            //Debug.Log("doopy");
            ysortScript.offset = -1;
            ysortScript.Sort();
            wasSouth = true;

        }
        else
        {
            if (wasSouth)
            {
                //Debug.Log("boopy");
                ysortScript.offset = offStore;
                ysortScript.Sort();
                wasSouth = false;
            }
        }
    }
}
