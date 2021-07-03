using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ysort : MonoBehaviour
{
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        Sort();
    }

    public void Sort()
    {
        GetComponent<Renderer>().sortingOrder = (int)(500f - (offset * 10f + transform.position.y * 10f));
    }


}
