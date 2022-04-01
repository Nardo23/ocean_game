using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

- Things to do:
    - - [ ] Actually implement the nice controller abstraction!
        - - [ ] Figure out what the fuck is up with the right stick on my controllers/why it's not working right.
    - - [ ] Make sure to set up all the Unity Input system axes for controllers and keyboards and mice
    - - [ ] Swap over all the code to use our InputAbstraction.inputInstance.GetButton functions
    - - [ ] Look over the code in menu.cs to figure out if I can comment it all out, it seems like the previous attempt to make it happen
    - - [ ] Implement a nice on-screen cursor that will appear/disappear when you're viewing the map or not!
        - - [ ] make it actually show/hide based on if the map is up or not
    - - [ ] Probably move the mouse and a bunch of the UI to be screen based not world based like the cursor.
    - - [ ] Implement keyboard/controller shortcuts for color and/or size/stamp/pen changes!
    - - [ ] Have the main gameplay fade into view on load possibly with a frame delay or so to help hide the player loading in.
    - - [ ] Make it so that the on screen cursor will change icons to match the pen/stamp mode? Probably worth making it partially transparent
    - - [ ] Ask Leo about remapping input, seems unlikely just because it would be text but who knows, Steam can handle that so it's not a massive issue
*/

public class InputAbstraction : MonoBehaviour
{
    // basically I'm putting this layer in between the drawing code and whatever API I/you end up using for
    // actually getting the controller/mouse input! That way you can more easily support the Steam API or
    // Unity input or some other third party system without having to re-write your whole system.
    // This is basically Unity's input system abstraction but a layer divorced from it so that you can
    // swap it out for the steam API later :)

    public enum OceanGameInputType {
        MoveHorizontal, MoveVertical, PenX, PenY, QuitGame, ToggleMap, Draw, Erase
    }

    public static InputAbstraction instance = null;

    public static AbstractInputSystem inputInstance = null;
    public Vector2 mousePos = new Vector2();
    

    public void Awake() {
        if (instance == null) {
            instance = this;
            inputInstance = new UnityInputSystem();
            DontDestroyOnLoad(gameObject);
            Cursor.visible = false; // hide our puny human cursors and use the game's special cursor!
            return;
        } else {
            // destroy yourself!
            Destroy(gameObject); // possibly want to destroy the gameobject this is on but for now this works.
        }
    }

    public void Update() {
        inputInstance.Update(Time.deltaTime);
    }
}


public abstract class AbstractInputSystem {
    // this is using the default Unity system
    public abstract float GetAxisRaw(InputAbstraction.OceanGameInputType axis);
    public abstract float GetAxis(InputAbstraction.OceanGameInputType axis);
    public abstract bool GetButton(InputAbstraction.OceanGameInputType axis);
    public abstract bool GetButtonDown(InputAbstraction.OceanGameInputType axis);
    public abstract Vector2 GetMousePosition();
    public abstract void Update(float dt);
}

public class UnityInputSystem : AbstractInputSystem
{
    public Vector2 mousePosition = new Vector2(); // possibly start this centered?
    private float controllerSpeed = 250; // you're going to have to set this here currently, 
    // it should probably be user adjustable too but maybe not if you're going for simplicity.

    private float deadzone = .1f;

    [HideInInspector] public Vector3 oldPhysicalMousePosition = new Vector2();

    public UnityInputSystem() {
        mousePosition.Set(Screen.width/2, Screen.height/2); // start with the mouse centered!
        oldPhysicalMousePosition = Input.mousePosition;
    }

    public override float GetAxis(InputAbstraction.OceanGameInputType axis)
    {
        return GetAxisRaw(axis);
    }

    public override float GetAxisRaw(InputAbstraction.OceanGameInputType axis)
    {
        switch(axis) {
            case InputAbstraction.OceanGameInputType.MoveHorizontal:
                return Input.GetAxisRaw("Horizontal");
            case InputAbstraction.OceanGameInputType.MoveVertical:
                return Input.GetAxisRaw("Vertical");
            case InputAbstraction.OceanGameInputType.PenX:
                return mousePosition.x;
            case InputAbstraction.OceanGameInputType.PenY:
                return mousePosition.y;
        }
        Debug.LogWarning("Warning: Trying to get raw axis for not implemented axis:" + axis.ToString());
        return 0;
    }

    public override bool GetButton(InputAbstraction.OceanGameInputType axis)
    {
        switch(axis) {
            case InputAbstraction.OceanGameInputType.Draw:
                return Input.GetMouseButton(0) || Input.GetButton("Draw"); // Input.GetKey(KeyCode.Space) || 
            case InputAbstraction.OceanGameInputType.Erase:
                return Input.GetMouseButton(1) || Input.GetButton("Erase"); // Input.GetKey(KeyCode.Delete) ||
            case InputAbstraction.OceanGameInputType.ToggleMap:
                return Input.GetButton("Map"); // Input.GetKey(KeyCode.Escape) || 
            case InputAbstraction.OceanGameInputType.QuitGame:
                return Input.GetButton("QuitGame");
        }
        return false;
    }

    public override bool GetButtonDown(InputAbstraction.OceanGameInputType axis)
    {
        switch(axis) {
            case InputAbstraction.OceanGameInputType.Draw:
                return Input.GetMouseButtonDown(0) || Input.GetButtonDown("Draw"); // Input.GetKey(KeyCode.Space) || 
            case InputAbstraction.OceanGameInputType.Erase:
                return Input.GetMouseButtonDown(1) || Input.GetButtonDown("Erase"); // Input.GetKey(KeyCode.Delete) ||
            case InputAbstraction.OceanGameInputType.ToggleMap:
                return Input.GetButtonDown("Map"); // Input.GetKey(KeyCode.Escape) || 
            case InputAbstraction.OceanGameInputType.QuitGame:
                return Input.GetButton("QuitGame");
        }
        return false;
    }

    public override Vector2 GetMousePosition()
    {
        return mousePosition;
    }

    public float GetAxisDeadZone(string axis) {
        float val = Input.GetAxis(axis);
        if (val < -deadzone || val > deadzone) {
            return val;
        }
        return 0; // inside the deadzone!
    }

    public override void Update(float dt)
    {
        // update the mouse position mainly!
        // we're going to return mouse position in pixel coords, as though you called input.getmouseposition
        if (Input.mousePosition != oldPhysicalMousePosition) {
            // then the mouse moved!
            oldPhysicalMousePosition = Input.mousePosition;
            mousePosition = oldPhysicalMousePosition;
        } else {
            // consider if we moved controller input?
            // also consider ijkl probably!
            Vector2 mouseMovement = new Vector2(GetAxisDeadZone("DrawHorizontal"), GetAxisDeadZone("DrawVertical")) * controllerSpeed * dt;
            mousePosition += mouseMovement;
            // Debug.Log(GetAxisDeadZone("DrawHorizontal") + ", " + GetAxisDeadZone("DrawVertical") + " movement: " + mouseMovement);
            // make sure that it stays on screen!
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
        }
    }
}