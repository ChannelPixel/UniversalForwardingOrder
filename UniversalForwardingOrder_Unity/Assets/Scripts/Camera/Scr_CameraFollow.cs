using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CameraFollow : MonoBehaviour
{
    [Header("Camera Distance")]
    public Vector3 distanceVector;
    public float rotationX = 0;
    public float distanceDamp;

    [SerializeField]
    GameObject followingObject;

    private void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, followingObject.transform.position - distanceVector, distanceDamp*Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(rotationX,0,0));
    }
}
