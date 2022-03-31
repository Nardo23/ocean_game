using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drownedShrine : MonoBehaviour
{
    public GameObject shrine;
    BoxCollider2D col;
    public AudioHighPassFilter highPass;
    public AudioSource sor;
    private void Start()
    {
        shrine.SetActive(false);
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    public void setShrine()
    {
        shrine.SetActive(true);
        col.enabled = true;
    }
    public void highOff()
    {
        highPass.enabled = false;
        sor.volume = .92f;
    }

    public void reOrder()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -3;

    }
}
