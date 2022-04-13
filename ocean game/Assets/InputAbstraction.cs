using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public delegate void VectorEvent(Vector2 move);
    public delegate void BoolEvent(bool value);
    public delegate void ContextEvent(InputAction.CallbackContext value);
    public event ContextEvent moveInputEvent;
    public event ContextEvent moveCursorInputEvent;
    public event ContextEvent drawInputEvent;
    public event ContextEvent mapInputEvent;
    public event ContextEvent eraseInputEvent;
    

    public void Awake() {
        if (instance == null) {
            instance = this;
            inputInstance = new UnityNewInputSystem();
            inputInstance.Initialize(this, gameObject);
            DontDestroyOnLoad(gameObject);
            Cursor.visible = false; // hide our puny human cursors and use the game's special cursor!
            return;
        } else {
            // destroy yourself!
            Destroy(gameObject); // possibly want to destroy the gameobject this is on but for now this works.
        }
    }

    public void Start() {
        // if we haven't been destroyed then we should register c# events!

    }

    public void MoveInput(InputAction.CallbackContext move) {
        // Debug.Log(move.ReadValue<Vector2>());
        // moveInputEvent.Invoke(move.ReadValue<Vector2>());
        moveInputEvent.Invoke(move);
    }

    public void MoveCursor(InputAction.CallbackContext move) {
        // Debug.Log(move.ReadValue<Vector2>());
        // moveCursorInputEvent.Invoke(move.ReadValue<Vector2>());
        moveCursorInputEvent.Invoke(move);
    }

    public void DrawInput(InputAction.CallbackContext move) {
        // Debug.Log(move.ReadValue<bool>());
        // drawInputEvent.Invoke(move.ReadValue<bool>());
        drawInputEvent.Invoke(move);
    }

    public void EraseInput(InputAction.CallbackContext move) {
        // Debug.Log(move.ReadValue<bool>());
        // eraseInputEvent.Invoke(move.ReadValue<bool>());
        eraseInputEvent.Invoke(move);
    }

    public void MapInput(InputAction.CallbackContext move) {
        // Debug.Log(move.ReadValue<bool>());
        // mapInputEvent.Invoke(move.ReadValue<bool>());
        mapInputEvent.Invoke(move);
    }

    public void Update() {
        inputInstance.Update(Time.deltaTime);
    }
}


[System.Serializable]
public abstract class AbstractInputSystem {
    // this is using the default Unity system
    public abstract float GetAxisRaw(InputAbstraction.OceanGameInputType axis);
    public abstract float GetAxis(InputAbstraction.OceanGameInputType axis);
    public abstract bool GetButton(InputAbstraction.OceanGameInputType axis);
    public abstract bool GetButtonDown(InputAbstraction.OceanGameInputType axis);
    public abstract Vector2 GetMousePosition();
    public abstract void Update(float dt);
    public abstract void Initialize(InputAbstraction ia, GameObject entity); // idk for now do this
}


[System.Serializable]
public class UnityNewInputSystem : AbstractInputSystem
{
    public Vector2 mousePosition = new Vector2(); // possibly start this centered?
    private float controllerSpeed = 250; // you're going to have to set this here currently, 
    // it should probably be user adjustable too but maybe not if you're going for simplicity.

    private float deadzone = .1f;

    [HideInInspector] public Vector3 oldPhysicalMousePosition = new Vector2();

    private Vector2 moveInput = new Vector2();
    private Vector2 cursorInput = new Vector2();
    private bool drawInput = false;
    private bool drawPreviousInput = false;
    private bool eraseInput = false;
    private bool erasePreviousInput = false;
    private bool mapInput = false;
    private bool mapPreviousInput = false;

    public UnityNewInputSystem() {
        mousePosition.Set(Screen.width/2, Screen.height/2); // start with the mouse centered!
        oldPhysicalMousePosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
    }

    public override void Initialize(InputAbstraction ia, GameObject entity) {
        PlayerInput pi = entity.GetComponent<PlayerInput>();

        ia.drawInputEvent += SetDrawInput;
        ia.mapInputEvent += SetMapInput;
        ia.eraseInputEvent += SetEraseInput;
        ia.moveCursorInputEvent += SetCursorMoveInput;
        ia.moveInputEvent += SetMoveInput;

        // SubscribeToInput(pi, "Draw").AddListener(SetDrawInput);
        // SubscribeToInput(pi, "Map").AddListener(SetMapInput);
        // SubscribeToInput(pi, "Erase").AddListener(SetEraseInput);
        // SubscribeToInput(pi, "Move").AddListener(SetMoveInput);
        // SubscribeToInput(pi, "MoveCursor").AddListener(SetCursorMoveInput);
    }

    public void SetCursorMoveInput(InputAction.CallbackContext callback) {
        cursorInput = callback.ReadValue<Vector2>();
    }

    public void SetMoveInput(InputAction.CallbackContext callback) {
        moveInput = callback.ReadValue<Vector2>();
    }

    public void SetDrawInput(InputAction.CallbackContext callback) {
        drawInput = callback.ReadValue<float>() != 0;
    }

    public void SetEraseInput(InputAction.CallbackContext callback) {
        eraseInput = callback.ReadValue<float>() != 0;
    }

    public void SetMapInput(InputAction.CallbackContext callback) {
        mapInput = callback.ReadValue<float>() != 0;
        Debug.Log("Map input " + mapInput);
    }

    private PlayerInput.ActionEvent SubscribeToInput(PlayerInput pi, string name) {
        for (int i = 0; i < pi.actionEvents.Count; i++) {
            if (pi.actionEvents[i].actionName.Equals(name)) {
                return pi.actionEvents[i];
            }
        }
        Debug.LogError("Can't find event name " + name + " did you change something?");
        return null;
    }

    public override float GetAxis(InputAbstraction.OceanGameInputType axis)
    {
        return GetAxisRaw(axis);
    }

    public override float GetAxisRaw(InputAbstraction.OceanGameInputType axis)
    {
        switch(axis) {
            case InputAbstraction.OceanGameInputType.MoveHorizontal:
                return moveInput.x; // replace input.
            case InputAbstraction.OceanGameInputType.MoveVertical:
                return moveInput.y; // replace input.
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
                return drawInput; // Input.GetKey(KeyCode.Space) || 
            case InputAbstraction.OceanGameInputType.Erase:
                return eraseInput; // Input.GetKey(KeyCode.Delete) ||
            case InputAbstraction.OceanGameInputType.ToggleMap:
                return mapInput; // Input.GetKey(KeyCode.Escape) || 
            case InputAbstraction.OceanGameInputType.QuitGame:
                // return Input.GetButton("QuitGame"); // idk if we actually want this or not so ignoring for now we don't use it anywhere
                return false;
        }
        return false;
    }

    public override bool GetButtonDown(InputAbstraction.OceanGameInputType axis)
    {
        switch(axis) {
            case InputAbstraction.OceanGameInputType.Draw:
                return drawInput;// && !drawPreviousInput; // Input.GetKey(KeyCode.Space) || 
            case InputAbstraction.OceanGameInputType.Erase:
                return eraseInput;// && !erasePreviousInput; // Input.GetKey(KeyCode.Delete) ||
            case InputAbstraction.OceanGameInputType.ToggleMap:
                return mapInput && !mapPreviousInput; // Input.GetKey(KeyCode.Escape) || 
            case InputAbstraction.OceanGameInputType.QuitGame:
                // return Input.GetButton("QuitGame");
                return false;
        }
        return false;
    }

    public override Vector2 GetMousePosition()
    {
        return mousePosition;
    }

    // public float GetAxisDeadZone(string axis) {
    //     float val = Input.GetAxis(axis);
    //     if (val < -deadzone || val > deadzone) {
    //         return val;
    //     }
    //     return 0; // inside the deadzone!
    // }

    public override void Update(float dt)
    {
        // update the mouse position mainly!
        // we're going to return mouse position in pixel coords, as though you called input.getmouseposition
        Vector2 mouseDelta = UnityEngine.InputSystem.Mouse.current.delta.ReadValue();
        if (mouseDelta.x != 0 || mouseDelta.y != 0) {
            // then the mouse moved!
            // oldPhysicalMousePosition = Input.mousePosition;
            // mousePosition = oldPhysicalMousePosition;

            // mousePosition += mouseDelta; // it would be nice if this worked but I don't think it does the way we want it to. Unfortunate!
            mousePosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        }
        else
        {
            // consider if we moved controller input?
            // also consider ijkl probably!
            Vector2 mouseMovement = cursorInput * controllerSpeed * dt;
            mousePosition += mouseMovement;
            // Debug.Log(GetAxisDeadZone("DrawHorizontal") + ", " + GetAxisDeadZone("DrawVertical") + " movement: " + mouseMovement);
            // make sure that it stays on screen!
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
        }

        erasePreviousInput = eraseInput;
        mapPreviousInput = mapInput;
        drawPreviousInput = drawInput;
    }
}