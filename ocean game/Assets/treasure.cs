using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure : MonoBehaviour
{
    public Sprite openSprite;
    public GameObject Treasure;
    public GameObject UiButton;
    public GameObject indicator;
    public bool opened = false;


    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.transform.tag == "Player" && !opened)
        {
            opened = true;
            GetComponent<SpriteRenderer>().sprite = openSprite;
            Treasure.SetActive(true);
            UiButton.SetActive(true);
            indicator.SetActive(true);

        }
    }


}
