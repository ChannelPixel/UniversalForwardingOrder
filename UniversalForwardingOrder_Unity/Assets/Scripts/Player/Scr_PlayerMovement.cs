using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public float movementForce;
    public float hoverForce;

    public float freeRotateForce;
    public float freeRotateDamp;
    public float rotateDamp;

    Coroutine moveRoutine;
    Coroutine hoverRoutine;
    Coroutine unlockRotationRoutine;
    Coroutine resetRotationRoutine;

    Rigidbody playerRigidbody;

    private void OnEnable()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        playerRigidbody = null;
    }

    public void ToggleGravity()
    {
        playerRigidbody.useGravity = !playerRigidbody.useGravity;
    }

    public void StartHover(Scr_IInput inputRef)
    {
        hoverRoutine = StartCoroutine(Hover(inputRef));
    }

    public void StopHover()
    {
        if (hoverRoutine != null)
        {
            StopCoroutine(hoverRoutine);
        }
    }

    public void StartMove(Scr_IInput inputRef)
    {
        moveRoutine = StartCoroutine(Move(inputRef));
    }

    public void StopMove()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
    }

    public void UnlockRotation(Scr_IInput inputRef)
    {
        unlockRotationRoutine = StartCoroutine(Rotate(inputRef));
    }

    public void LockRotation()
    {
        if(unlockRotationRoutine != null)
        {
            StopCoroutine(unlockRotationRoutine);
        }
    }

    public void ResetRotation()
    {
        resetRotationRoutine = StartCoroutine(ResetRotate());
    }

    public void StopResetRotation()
    {
        if(resetRotationRoutine != null)
        {
            StopCoroutine(resetRotationRoutine);
        }
    }

    IEnumerator Hover(Scr_IInput inputRef)
    {
        while(true)
        {
            float hoverMult = hoverForce * playerRigidbody.mass;
            Vector3 hoverVector = new Vector3( 0, hoverMult, 0);
            playerRigidbody.AddRelativeForce(hoverVector * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator Move(Scr_IInput inputRef)
    {
        while(true)
        {
            //Adds force to move the player.
            float moveMult = movementForce * playerRigidbody.mass;
            Vector3 moveVector = new Vector3(inputRef.InputVector.x * moveMult, 0, inputRef.InputVector.z * moveMult);
            playerRigidbody.AddForce(moveVector * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator Rotate(Scr_IInput inputRef)
    {
        while(true)
        {
            float freeRotateMult = freeRotateForce * playerRigidbody.mass;
            playerRigidbody.AddTorque(new Vector3(inputRef.InputVector.z * freeRotateMult, 0,-inputRef.InputVector.x * freeRotateMult));

            yield return null;
        }
    }

    IEnumerator ResetRotate()
    {
        while(true)
        {

            playerRigidbody.transform.rotation = Quaternion.Slerp(playerRigidbody.transform.rotation, Quaternion.identity, playerRigidbody.mass * Time.deltaTime);

            yield return null;
        }
    }
}
