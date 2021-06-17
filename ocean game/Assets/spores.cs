using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spores : MonoBehaviour
{
    public GameObject sporesObj;

    private void Start()
    {
        sporesObj.SetActive(false);
    }

    public void SporesPlay()
    {
        sporesObj.SetActive(true);
    }

    public void SporesStop()
    {
        sporesObj.SetActive(false);
    }


}
