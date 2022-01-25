using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setOn : MonoBehaviour
{
    public GameObject obj;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && obj.activeSelf ==false)
        {
            obj.SetActive(true);
        }
    }
}
