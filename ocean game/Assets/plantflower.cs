using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantflower : MonoBehaviour
{
    public GameObject spriteObj;
    public bool canToggle = true;
    [SerializeField]
    Sprite[] flowers;
    [SerializeField]
    Sprite[] mushrooms;
    public bool inUnderworld = false;

    // Start is called before the first frame update
    void Start()
    {
        assignSprite(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void assignSprite()
    {
        if (inUnderworld)
        {
            if (Random.Range(1, 3) > 1)
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = mushrooms[Random.Range(0, mushrooms.Length)];
            }
            else
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = flowers[Random.Range(0, flowers.Length)];
            }
        }
        else
        {
            if (Random.Range(1, 4) > 3)
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = mushrooms[Random.Range(0, mushrooms.Length)];
            }
            else
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = flowers[Random.Range(0, flowers.Length)];
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (canToggle)
            {
                spriteObj.SetActive(!spriteObj.activeSelf);
            }
                

            if (spriteObj.activeSelf == true)
            {
                //sor.PlayOneShot(fireStartSound);

            }
        }
    }

}
