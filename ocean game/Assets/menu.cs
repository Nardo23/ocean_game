using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public map mapScript;
    public GameObject newCursor;
    Vector2 cursorPos;
    public Vector3 prevPos;
    Animator CursorAnim;
    bool click = false;
    public Camera cam;
    float timer;
    public bool titleOrCred = false;
    
    float xInput, yInput;
    public float speed;

    // so that I don't modify your scene too much I'm going to find it on start
    private GraphicRaycaster raycaster = null;
    private UnityEngine.EventSystems.EventSystem eventSystem;
    private Selectable[] previousSelected = {};


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        Cursor.visible = false;
        CursorAnim = newCursor.GetComponent<Animator>();
        
        raycaster = gameObject.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
        // it should also probably disable/remove the standalone input system since we're doing our own
        // thing but tell leo that TODO FIX!
        Debug.LogWarning("HEY JORDAN/LEO, PROBABLY REMOVE THE STANDALONE EVENT SYSTEM");
        // if (GameObject.FindObjectOfType<UnityEngine.EventSystems.StandaloneInputModule>()) {
        //     GameObject.FindObjectOfType<UnityEngine.EventSystems.StandaloneInputModule>().enabled = false;
        // }
        newCursor.GetComponent<SpriteRenderer>().enabled = false; // by default hide the cursor for the main menu for instance!
    }

    // Update is called once per frame
    async void Update()
    {

        // CursorMove();

        if (click)
        {
            click = false;
            //CursorAnim.ResetTrigger("click");
        }
        if (!titleOrCred)
        {
            if (InputAbstraction.inputInstance.GetButtonDown(InputAbstraction.OceanGameInputType.ToggleMap))
            // if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
                mapScript.mapButton();
                timer = 0;
                // set the cursor visible to whether or not we're in the map!
                newCursor.GetComponent<SpriteRenderer>().enabled = mapScript.mapBackground.activeSelf;

            }
        }
        
        // currently just using the draw for everything, this should be made better though!
        if (InputAbstraction.inputInstance.GetButtonDown(InputAbstraction.OceanGameInputType.Draw))
        {
            CursorAnim.SetTrigger("click");
            click = true;
            timer = 0;
            newCursor.GetComponent<SpriteRenderer>().enabled = true;
        }
        

        // move the cursor to the mouse input position!
        // this cursor seems to be a world UI object or something? Hmmm.
        Vector2 mousePos = InputAbstraction.inputInstance.GetMousePosition();
        prevPos = Camera.main.ScreenToWorldPoint(mousePos);
        prevPos.z = 0; // using this public variable so that I don't have to make a new one.
        newCursor.transform.position = prevPos;

        // handle clicks! Here we should handle clicks with the fake cursor!
        if (eventSystem != null) {
            // we currently need an event system to implement the fake clicks, but currently
            // we have one in the main scene where we need it so it works!
            UnityEngine.EventSystems.PointerEventData pd = new UnityEngine.EventSystems.PointerEventData(eventSystem);
            pd.position = mousePos;
            List<UnityEngine.EventSystems.RaycastResult> results = new List<UnityEngine.EventSystems.RaycastResult>();
            raycaster.Raycast(pd, results);
            for (int i = 0; i < results.Count; i++) {
                Selectable[] selectables = results[i].gameObject.GetComponents<Selectable>();
                for (int j = 0; j < selectables.Length; j++) {
                    if (click) {
                        selectables[j].Select(); // click whatever buttons we find!
                    }
                    // else {
                    //     selectables[j].OnPointerEnter(pd);
                    // }
                }
                // if (selectables.Length > 0) {
                //     break;
                // }
            }
        }
    }

    // void CursorMove()
    // {
        

    //     xInput = Input.GetAxis("RightStickHorizontal");
    //     yInput = Input.GetAxis("RightStickVertical");

    //     if (xInput == 0 && yInput == 0)
    //     {
    //         cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         newCursor.transform.position = cursorPos;
   
    //     }
    //     else
    //     {
             
    //         cursorPos = new Vector2(cursorPos.x + xInput * speed, cursorPos.y + yInput * speed);
    //         newCursor.transform.position = cursorPos;
    //         //WarpCursorPosition(new Vector2());
    //         //cursorPos = cam.WorldToScreenPoint(newCursor.transform.position);
    //     }

    //     if (prevPos == Input.mousePosition && !Input.GetMouseButtonDown(0))
    //     {
    //         //Debug.Log("weeeeoeoe");
    //         if (timer <= 2.5)
    //             timer += Time.deltaTime;
    //         if (timer > 2.5)
    //         {
    //             timer = 0;
    //             newCursor.GetComponent<SpriteRenderer>().enabled = false;
    //         }
            
    //     }
    //     else
    //     {
    //         timer = 0;
    //         newCursor.GetComponent<SpriteRenderer>().enabled = true;
    //     }
    //     //Debug.Log("mouse time: "+timer);

    //     prevPos = Input.mousePosition;
    // }



}
