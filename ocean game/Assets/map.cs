using FreeDraw;
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
    public GameObject drawCanvas;

    public GameObject indicator;
    float height;
    float width;
    public float xoff = 55, yoff = 32;

    public Drawable drawableScript;
    public DrawingSettings drawSetScript;

    private bool loaded = false;
    public AudioSource sor;
    public AudioClip close;
    public AudioClip open;
    // Start is called before the first frame update
    void Start()
    {
        
        height = mapBackground.GetComponent<RectTransform>().sizeDelta.y; 
        width = mapBackground.GetComponent<RectTransform>().sizeDelta.x;
        playerScript = player.GetComponent<Player>();
        
        drawCanvas.SetActive(false);
        mapBackground.SetActive(false);
        loaded = true;
    }

    public void openMap()
    {
        
        if (mapBackground.activeSelf)   // if closing map, save before dissabling !
        {
            if (loaded)
            {                
                drawableScript.Save();
                drawSetScript.saveStamps();
            }
        }

        // now we can toggle map
        playerScript = player.GetComponent<Player>();
        mapBackground.SetActive(!mapBackground.activeSelf);
        drawCanvas.SetActive(!drawCanvas.activeSelf);
        if (mapBackground.activeSelf)
        {
            indicator.SetActive(false);
            playerScript.canMove = false;

            if (!playerScript.inUnderworld)
            {
                updateMap();
            }
            else
            {
                updateMapUnderworld();
            }
            sor.PlayOneShot(open);
        }
        else
        {
            sor.PlayOneShot(close);
            playerScript.canMove = true;
        }
    }

    public void updateMap()
    {
        float mapX = (player.transform.position.x / (worldMax.x - worldMin.x)) * width;

        float mapY = (player.transform.position.y / (worldMax.y - worldMin.y)) * height;

        //GetComponent<RectTransform>().localPosition = new Vector3 (mapX-350, mapY-110, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(mapX - xoff, mapY - (yoff), 0);
    }

    public void updateMapUnderworld()
    {
        float mapX = (playerScript.doorStore.x / (worldMax.x - worldMin.x)) * width;

        float mapY = (playerScript.doorStore.y / (worldMax.y - worldMin.y)) * height;

        //GetComponent<RectTransform>().localPosition = new Vector3 (mapX-350, mapY-110, 0);
        GetComponent<RectTransform>().localPosition = new Vector3(mapX - xoff, mapY - (yoff), 0);
    }


    void OnDissable()
    {
        Debug.Log("OnDisaable");
        if (loaded)
        {
            Debug.Log("OnDisaableLoad");
            drawableScript.Save();
            drawSetScript.saveStamps();
        }
        
    }


    // update just for testing comment out update for build
  //  void update()
   // {
   //     updateMap();
   // }

}
