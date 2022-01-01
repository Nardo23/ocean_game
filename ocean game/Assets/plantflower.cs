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
    AudioSource sor;
    [SerializeField]
    AudioClip jarSound;
    [SerializeField]
    AudioClip[] mushSound;
    [SerializeField]
    AudioClip[] flowerSound;
    AudioClip plantSound;
    public bool inUnderworld = false;
    AudioReverbFilter reverb;
    float timer = 0;
    bool off = false;
    // Start is called before the first frame update
    void Start()
    {
        assignSprite();
        sor = GetComponent<AudioSource>();
        reverb = GetComponent<AudioReverbFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < .5f &&off)
        {
            timer += Time.deltaTime;
        }
        else if(timer >=.5f && off)
        {
            timer = 0;
            off = false;
        }
    }

    void assignSprite()
    {
        if (inUnderworld)
        {
            if (Random.Range(1, 3) > 1)
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = mushrooms[Random.Range(0, mushrooms.Length-1)];
                plantSound = mushSound[Random.Range(0, mushSound.Length)];
            }
            else
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = flowers[Random.Range(0, flowers.Length)];
                plantSound = flowerSound[Random.Range(0, flowerSound.Length)];
            }
        }
        else
        {
            if (Random.Range(1, 4) > 3)
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = mushrooms[Random.Range(0, mushrooms.Length-1)];
                plantSound = mushSound[Random.Range(0, mushSound.Length)];
            }
            else
            {
                spriteObj.GetComponent<SpriteRenderer>().sprite = flowers[Random.Range(0, flowers.Length)];
                plantSound = flowerSound[Random.Range(0, flowerSound.Length)];
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (canToggle && !off)
            {
                off = true;
                if (inUnderworld)
                {
                    reverb.enabled = true;
                }
                else
                {
                    reverb.enabled = false;
                }
                spriteObj.SetActive(!spriteObj.activeSelf);
                sor.pitch = Random.Range(.85f, 1.15f);
                sor.PlayOneShot(jarSound);
                if (spriteObj.activeSelf == true)
                {
                    sor.PlayOneShot(plantSound);
                    //sor.PlayOneShot(fireStartSound);

                }

            }
                

            
        }
    }

}
