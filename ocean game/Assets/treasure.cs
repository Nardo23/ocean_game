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
    public bool givenTreasure = false;
    public int id =-1;
    bool awoken = false;
    TreasureManager treasureManagerScript;

    AudioSource sor;
    public AudioClip treasureClip;
    public AudioClip noTreasureClip;

    // Start is called before the first frame update

    void Start()
    {
        sor = GetComponent<AudioSource>();

    }

    void Awake()
    {
        treasureManagerScript = GameObject.Find("TreasureManagerObj").GetComponent<TreasureManager>();
        if (treasureManagerScript.checkChest(transform.position.x, transform.position.y) && !awoken)
        {
            Debug.Log("TreasureOPened");
            awoken = true;
            givenTreasure = true;
            opened = true;
            GetComponent<SpriteRenderer>().sprite = openSprite;
            UiButton.SetActive(true);
            Treasure.SetActive(false);

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.transform.tag == "Player" && !opened)
        {
            sor.PlayOneShot(treasureClip);
            opened = true;
            givenTreasure = true;
            GetComponent<SpriteRenderer>().sprite = openSprite;
            Treasure.SetActive(true);
            UiButton.SetActive(true);
            indicator.SetActive(true);

            

        }
        else if (opened)
        {
            sor.PlayOneShot(noTreasureClip);
        }
    }


}
