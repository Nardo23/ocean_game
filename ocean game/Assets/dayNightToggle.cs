using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightToggle : MonoBehaviour
{
    [SerializeField]
    GameObject[] dayObjs;
    [SerializeField]
    GameObject[] nightObjs;


    private void Start()
    {
        for (int i = 0; i < dayObjs.Length; i++)
        {
            dayObjs[i].SetActive(true);
        }
        for (int i = 0; i < nightObjs.Length; i++)
        {
            nightObjs[i].SetActive(false);
        }
    }






    public void toggle(bool day)
    {

        StartCoroutine(ExampleCoroutine(day));

    }

    public void endToggle(bool day)
    {

        for (int i = 0; i < dayObjs.Length; i++)
        {
            dayObjs[i].SetActive(day);
        }
        for (int i = 0; i < nightObjs.Length; i++)
        {
            nightObjs[i].SetActive(!day);
        }
    }


    IEnumerator ExampleCoroutine(bool day)
    {
       
        yield return new WaitForSeconds(12);

        endToggle(day);
    }


}
