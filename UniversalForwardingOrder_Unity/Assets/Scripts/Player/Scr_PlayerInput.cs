using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scr_PlayerInput : MonoBehaviour, Scr_IInput
{
    Vector3 inputVector;
    Vector3 lastInputVector;

    //MOUSE
    bool inputLMB;
    bool lastInputLMB;
    bool inputRMB;
    bool lastInputRMB;
    bool inputMMB;
    bool lastInputMMB;

    //MOUSEWHEEL
    float inputScrollWheel;
    float lastScrollWheel;

    //KEYBOARD
    bool isWASD;
    bool lastIsWASD;
    bool inputJump;
    bool lastInputJump;
    bool inputShift;
    bool lastInputShift;
    bool inputE;
    bool lastInputE;
    bool inputEsc;
    bool lastInputEsc;

    //CONDITIONS
    bool isJumping;
    bool isResetting;
    bool isMoving;
    bool isTilting;
    bool isBeaming;

    Vector3 Scr_IInput.InputVector
    {
        get { return inputVector; }
    }

    public Scr_EventTypes.InputEvent startHover;
    public UnityEvent stopHover;
    public Scr_EventTypes.InputEvent startMove;
    public UnityEvent stopMove;
    public Scr_EventTypes.InputEvent unlockRotation;
    public UnityEvent lockRotation;
    public UnityEvent startReset;
    public UnityEvent stopReset;
    public UnityEvent startBeam;
    public UnityEvent stopBeam;
    public UnityEvent scrollUpObjects;
    public UnityEvent scrollDownObjects;
    public UnityEvent fireBeam;
    public UnityEvent toggleGravity;

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");//Read WS or ^V keys
        inputVector.z = Input.GetAxisRaw("Vertical");//Read AD or <> keys
        inputLMB = Input.GetButton("LMB");
        inputMMB = Input.GetButton("MMB");
        inputScrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        inputRMB = Input.GetButton("RMB");
        inputShift = Input.GetButton("Shift");
        inputJump = Input.GetButton("Jump");
        inputE = Input.GetButton("E");
        inputEsc = Input.GetKeyDown(KeyCode.Escape);

        if(!inputEsc)
        {
            if (inputScrollWheel == 1)
            {
                print("Scroll UP");
                scrollUpObjects.Invoke();
            }

            if (inputScrollWheel == -1)
            {
                print("Scroll DOWN");
                scrollDownObjects.Invoke();
            }

            if (inputLMB
                && !lastInputLMB)
            {
                print("Fire");
                fireBeam.Invoke();
            }

            if (inputRMB
                && !lastInputRMB)
            {
                print("Beam on.");
                startBeam.Invoke();
            }

            if (!inputRMB
                && lastInputRMB)
            {
                print("Beam off.");
                stopBeam.Invoke();
            }

            if (inputMMB //Player currently pressing MIDDLE MOUSE BUTTON this frame
                && !lastInputMMB) //NOT Player currently pressed MIDDLE MOUSE BUTTON last frame
            {
                startReset.Invoke();
            }

            if (!inputMMB //NOT Player currently pressing MIDDLE MOUSE BUTTON this frame
                && lastInputMMB) //Player currently pressed MIDDLE MOUSE BUTTON last frame
            {
                stopReset.Invoke();
            }

            if (inputE
                && !lastInputE)
            {
                toggleGravity.Invoke();
            }


            if (inputVector.magnitude > 0f //Player currently pressing WASD this frame
                && lastInputVector.magnitude == 0f) //Player hasn't pressed WASD last frame
            {
                isWASD = true;
            }

            if (inputVector.magnitude == 0f //Player isnt currently pressing WASD this frame
                && lastInputVector.magnitude > 0f) //Player pressed WASD last frame
            {
                isWASD = false;
            }

            if (inputJump //Player currently pressing SPACE this frame
                && !lastInputJump) //NOT Player pressed SPACE last frame
            {
                startHover.Invoke(this);
                isJumping = true;
            }

            if (!inputJump //NOT Player currently pressing space this frame
                && lastInputJump) //Player pressed SPACE last frame
            {
                stopHover.Invoke();
            }

            if (inputShift &&
                !lastInputShift)
            {
                unlockRotation.Invoke(this);
                isTilting = true;
            }

            if (!inputShift &&
                lastInputShift)
            {
                lockRotation.Invoke();
                isTilting = false;
            }

            if (isWASD &&
                !lastIsWASD &&
                !inputShift ||

                isWASD &&
                lastIsWASD &&
                !inputShift &&
                lastInputShift)
            {
                startMove.Invoke(this);
                isMoving = true;
            }

            if (!isWASD &&
                lastIsWASD ||
                isTilting)
            {
                stopMove.Invoke();
                isMoving = false;
            }

            lastInputE = inputE;
            lastIsWASD = isWASD;
            lastInputVector = inputVector;
            lastInputLMB = inputLMB;
            lastInputMMB = inputMMB;
            lastInputRMB = inputRMB;
            lastInputShift = inputShift;
            lastInputJump = inputJump;
        }

        lastInputEsc = inputEsc;
    }
}
