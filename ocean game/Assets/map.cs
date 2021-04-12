using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public Vector2 worldMin;
    public Vector2 worldMax;
    public GameObject player;
    public GameObject mapBackground;
    float height;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        height = mapBackground.GetComponent<RectTransform>().sizeDelta.y; 
        width = mapBackground.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        float mapX = (player.transform.position.x / (worldMax.x - worldMin.x))*width;

        float mapY = (player.transform.position.y / (worldMax.y - worldMin.y)) * height;

        //GetComponent<RectTransform>().localPosition = new Vector3 (mapX-350, mapY-110, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(mapX-360, mapY- (height/5), 0);
    }

}
