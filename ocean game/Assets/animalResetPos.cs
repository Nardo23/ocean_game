using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalResetPos : MonoBehaviour
{
    public Transform rat;
    public Transform possum;
    private Vector2 ratStore;
    private Vector2 posStore;
    public Transform over;
    private void Start()
    {
        posStore = new Vector2 (possum.position.x + over.position.x, possum.position.y +over.position.y);
        ratStore = new Vector2(rat.position.x + over.position.x, rat.position.y + over.position.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            possum.position = posStore;
            rat.position = ratStore;
        }
    }
}
