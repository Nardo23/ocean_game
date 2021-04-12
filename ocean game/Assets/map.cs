using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public Vector2 worldMin;
    public Vector2 worldMax;
    public GameObject player;
    Player playerScript;
    public GameObject mapBackground;
    float height;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        height = mapBackground.GetComponent<RectTransform>().sizeDelta.y; 
        width = mapBackground.GetComponent<RectTransform>().sizeDelta.x;
        playerScript = player.GetComponent<Player>();
    }

    public void openMap()
    {
        //Debug.Log("piss");
        playerScript = player.GetComponent<Player>();
        mapBackground.SetActive(!mapBackground.activeSelf);
        if (mapBackground.activeSelf)
        {
            playerScript.canMove = false;

            if (!playerScript.inUnderworld)
            {
                updateMap();
            }
            else
            {

            }
            
        }
        else
        {
            playerScript.canMove = true;
        }
    }

    public void updateMap()
    {
        float mapX = (player.transform.position.x / (worldMax.x - worldMin.x)) * width;

        float mapY = (player.transform.position.y / (worldMax.y - worldMin.y)) * height;

        //GetComponent<RectTransform>().localPosition = new Vector3 (mapX-350, mapY-110, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(mapX - 360, mapY - (height / 5), 0);
    }

    public void updateMapUnderworld()
    {
        float mapX = (playerScript.doorStore.x / (worldMax.x - worldMin.x)) * width;

        float mapY = (playerScript.doorStore.y / (worldMax.y - worldMin.y)) * height;

        //GetComponent<RectTransform>().localPosition = new Vector3 (mapX-350, mapY-110, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(mapX - 360, mapY - (height / 5), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
