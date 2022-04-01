using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penguin : MonoBehaviour
{
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("offset", offset);
    }
}
