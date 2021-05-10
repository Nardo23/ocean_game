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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        CursorAnim = newCursor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newCursor.transform.position = cursorPos;

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
}
