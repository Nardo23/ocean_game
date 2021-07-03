using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    public GameObject heartObj;
    public campFire fireScript;

    // Start is called before the first frame update
    void Start()
    {
        
        heartObj.SetActive(true);
        fireScript.canToggle = false;
    }
    
}
