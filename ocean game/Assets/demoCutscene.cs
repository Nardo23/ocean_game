using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoCutscene : MonoBehaviour
{
    public Player playerScript;
    public GameObject camTranObj;
    public rain rainScript;
    public Color camColor = new Color(255f, 255f, 255f, 255f);
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void playScene()
    {
        transform.parent = null;
        transform.position = new Vector3(playerScript.transform.position.x, playerScript.transform.position.y, -10);
        anim.SetTrigger("play2");
    }


    void setSnow()
    {
        playerScript.snow = true;
        rainScript.raining = true;
    }
    void endSnow()
    {
        playerScript.snow = false;
        rainScript.raining = false;
    }

    void fadecam()
    {
        camTranObj.GetComponent<SpriteRenderer>().color = camColor;
        camTranObj.SetActive(true);
    }
    void swapWorld()
    {
        playerScript.SwapWorld();
    }

}
