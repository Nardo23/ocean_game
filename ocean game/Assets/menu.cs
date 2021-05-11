using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public map mapScript;
    public GameObject newCursor;
    Vector2 cursorPos;
    Animator CursorAnim;
    bool click = false;
    public Camera cam;
    

    float xInput, yInput;
    public float speed;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        CursorAnim = newCursor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CursorMove();

        if (click)
        {
            click = false;
            //CursorAnim.ResetTrigger("click");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            mapScript.openMap();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CursorAnim.SetTrigger("click");
            click = true;
        }
        

    }

    void CursorMove()
    {
        

        xInput = Input.GetAxis("RightStickHorizontal");
        yInput = Input.GetAxis("RightStickVertical");

        if (xInput == 0 && yInput == 0)
        {
            cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newCursor.transform.position = cursorPos;
        }
        else
        {
            
            cursorPos = new Vector2(cursorPos.x + xInput * speed, cursorPos.y + yInput * speed);
            newCursor.transform.position = cursorPos;
            WarpCursorPosition(new Vector2());
            cursorPos = cam.WorldToScreenPoint(newCursor.transform.position);
        }



    }



}
