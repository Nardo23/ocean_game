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
    public GameObject button;
    float height;
    float width;
    public float xoff = 55, yoff = 32;

    public Drawable drawableScript;
    public DrawingSettings drawSetScript;

    private bool loaded = false;
    public AudioSource sor;
    public AudioClip close;
    public AudioClip open;

    public Animator buttonAnim;
    public endArea endScript;

    public bool ending = false;
    public SpriteRenderer normalCursorRend, drawCursorRend;


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

    public void endingState()
    {
        buttonAnim.SetTrigger("blink");
        ending = true;
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
            InputAbstraction.inputInstance.AllowControllerMoveCursor(true); // opened the map!
            buttonAnim.SetTrigger("close");  // button shows closed sprite when map is open
            indicator.SetActive(false);
            playerScript.canMove = false;

            normalCursorRend.enabled = false; //switch to drawing tools cursor
            drawCursorRend.enabled = true;

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
            InputAbstraction.inputInstance.AllowControllerMoveCursor(false); // closed the map!
            normalCursorRend.enabled = true; //switch back to arrow cursor
            drawCursorRend.enabled = false;
            buttonAnim.SetTrigger("open"); // button shows open sprite when map is closed
            sor.PlayOneShot(close);
            playerScript.canMove = true;
        }
    }

    public void mapButton()
    {
        //buttonAnim.Play("button idle");
        if (ending)
        {
            button.SetActive(false);
            endScript.bottle();
        }
        else
        {
            openMap();
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
        //Debug.Log("OnDisaable");
        if (loaded)
        {
            //Debug.Log("OnDisaableLoad");
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
