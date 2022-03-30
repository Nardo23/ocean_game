using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaPlatform : MonoBehaviour
{
    EdgeCollider2D edge;
    CapsuleCollider2D cap;
    public GameObject floorCols;
    public GameObject floor;
    public SpriteRenderer sprt;
    public SpriteRenderer sprt2;
    public SpriteRenderer sprt3;
    public Collider2D tileColliders;

    // Start is called before the first frame update
    void Start()
    {
        floorCols.SetActive(false);
        edge = GetComponent<EdgeCollider2D>();
        cap = GetComponent<CapsuleCollider2D>();
        floorCols.SetActive(false);
        edge.enabled = false;
        cap.enabled = true;
        sprt.sortingLayerName = "ui";
        sprt2.sortingLayerName = "ui";
        sprt3.sortingLayerName = "ui";
        sprt3.sortingOrder = -2000;
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        GetComponent<ysort>().enabled = true;
        childToggle(false);
        floor.GetComponent<ysort>().enabled = true;

    }


   void childToggle(bool onOff)
    {
        int i = 0;
        Transform[] allChildren = floor.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            
            if (child.GetComponent<CircleCollider2D>() != null)
            {
                child.GetComponent<CircleCollider2D>().enabled = onOff;
            }
            if (child.GetComponent<BoxCollider2D>() != null)
            {
                child.GetComponent<BoxCollider2D>().enabled = onOff;
            }
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                if (!onOff)
                {
                    child.GetComponent<SpriteRenderer>().sortingLayerName = "ui";
                    if(child.GetComponent<ysort>() != null)
                    {
                        child.GetComponent<ysort>().enabled = false;
                        child.GetComponent<SpriteRenderer>().sortingOrder = 0 +i ;
                        i++;
                    }
                }
                else
                {
                    child.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                    if (child.GetComponent<ysort>() != null)
                    {
                        child.GetComponent<ysort>().enabled = true;
                        child.GetComponent<ysort>().Sort();


                    }
                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            floorCols.SetActive(true);
            edge.enabled = true;
            cap.enabled = false;
            sprt.sortingLayerName = "Default";
            sprt2.sortingLayerName = "Default";
            sprt3.sortingLayerName = "Default";
            GetComponent<SpriteRenderer>().sortingLayerName = "tile";
            GetComponent<ysort>().enabled = false;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            childToggle(true);
            floor.GetComponent<ysort>().enabled = true;
            floor.GetComponent<ysort>().Sort();
            tileColliders.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("EXIT");
            floorCols.SetActive(false);
            edge.enabled = false;
            cap.enabled = true;
            sprt.sortingLayerName = "ui";
            sprt2.sortingLayerName = "ui";
            sprt3.sortingLayerName = "ui";
            sprt3.sortingOrder = -2000;
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            GetComponent<ysort>().enabled = true;
            GetComponent<ysort>().Sort();
            childToggle(false);
            floor.GetComponent<ysort>().enabled = true;
            floor.GetComponent<ysort>().Sort();
            tileColliders.enabled = true;
        }
    }

}
