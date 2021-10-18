using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableobj : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    public void disable()
    {
        obj.SetActive(false);
    }
}
