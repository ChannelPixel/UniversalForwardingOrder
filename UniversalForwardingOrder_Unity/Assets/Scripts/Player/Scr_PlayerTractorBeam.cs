using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scr_PlayerTractorBeam : MonoBehaviour
{
    [Header("Tractor Beam Stats")]
    public float rotationDamp = 20;
    public float pullSpeed = 10;
    public LayerMask pullMask;
    public float fireForce = 5000;

    [Header("Pull Location Stats")]
    public GameObject parentObject;
    public GameObject pullLocationObject;
    public Vector3 globalPullLocation;

    [Header("Selected Object Info")]
    public GameObject selectedObject;
    public Quaternion selectedObjectPulledRotation;
    public Vector3 selectedObjectVector;
    public Vector3 lastSelectedObjectVector;
    public Vector3 exitSelectedObjectVector;
    public float exitForce = 2000;

    [Header("Items in Beam")]
    public int tractorableObjectSelectIndex = 0;
    public List<GameObject> tractorableObjects = new List<GameObject>();

    private void OnEnable()
    {       
        updateGlobalPullLocation();
    }

    private void OnDisable()
    {
        tangentVelocityExit();
        clearSelectedObject();

        tractorableObjectSelectIndex = 0;
        tractorableObjects = new List<GameObject>();
    }

    void clearSelectedObject()
    {
        if(selectedObject != null)
        {
            selectedObject.GetComponent<Rigidbody>().useGravity = true;
        }

        selectedObject = null;
        selectedObjectPulledRotation = new Quaternion();
        selectedObjectVector = new Vector3();
        lastSelectedObjectVector = new Vector3();
    }

    private void FixedUpdate()
    {
        updateGlobalPullLocation();

        if(gameObject.GetComponent<Collider>().enabled)
        {
            if (selectedObject != null 
                && selectedObject.GetComponent<Rigidbody>() != null)
            {
                selectedObject.GetComponent<Rigidbody>().useGravity = false;

                pullSelectedObject();
            }
        }
        exitSelectedObjectVector = -(lastSelectedObjectVector - selectedObjectVector);

        lastSelectedObjectVector = selectedObjectVector;
    }

    void updateGlobalPullLocation()
    {
        globalPullLocation = pullLocationObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ITractorable>() != null
            && other.GetComponent<Rigidbody>() != null)
        {
            tractorableObjects.Add(other.gameObject);
        }
        
        if(selectedObject == null 
            && tractorableObjects.Count >= 1)
        {
            selectedObject = tractorableObjects[0];
            selectedObjectPulledRotation = selectedObject.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other != null)
        {
            if (other.GetComponent<ITractorable>() != null)
            {
                if (selectedObject != null && selectedObject.GetComponent<Rigidbody>() != null)
                {
                    selectedObject.GetComponent<Rigidbody>().useGravity = true;
                    tangentVelocityExit();
                }

                if (selectedObject == other.gameObject)
                {
                    clearSelectedObject();
                }

                tractorableObjects.Remove(other.gameObject);

                if (selectedObject == null && tractorableObjects.Count >= 1)
                {
                    selectedObject = tractorableObjects[0];
                    selectedObjectPulledRotation = selectedObject.transform.rotation;
                }
            }
        }
    }
    
    void tractorableRefresh()
    {
        tractorableObjects = new List<GameObject>();
    }

    void pullSelectedObject()
    {
        //Move object to pullLocation
        if (selectedObject != null)
        {
            //selectedObject.GetComponent<Rigidbody>().MovePosition(Vector3.Slerp(selectedObject.transform.position, globalPullLocation, pullSpeed * Time.deltaTime));
            selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, globalPullLocation, pullSpeed * Time.deltaTime);
            selectedObject.transform.rotation = Quaternion.Slerp(selectedObject.transform.localRotation, selectedObjectPulledRotation * gameObject.transform.rotation, rotationDamp * Time.deltaTime);
            selectedObjectVector = selectedObject.transform.position;
        }
    }

    public void tractorablesCycleUp()
    {
        if(tractorableObjects.Count > 1)
        {
            if (tractorableObjectSelectIndex == tractorableObjects.Count - 1)
            {
                tractorableObjectSelectIndex = -1;
            }

            tractorableObjectSelectIndex++;
            selectedObject.GetComponent<Rigidbody>().useGravity = true;
            selectedObject = tractorableObjects[tractorableObjectSelectIndex];
            selectedObjectPulledRotation = selectedObject.transform.rotation;
        }
    }

    public void tractorablesCycleDown()
    {
        if(tractorableObjects.Count > 1)
        {
            if (tractorableObjectSelectIndex == 0)
            {
                tractorableObjectSelectIndex = tractorableObjects.Count;
            }

            tractorableObjectSelectIndex--;
            selectedObject.GetComponent<Rigidbody>().useGravity = true;
            selectedObject = tractorableObjects[tractorableObjectSelectIndex];
            selectedObjectPulledRotation = selectedObject.transform.rotation;
        }
    }

    void tangentVelocityExit()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            float exitMult =  exitForce * selectedObject.GetComponent<Rigidbody>().mass;
            Vector3 exitForceVector = exitSelectedObjectVector * exitMult;
            selectedObject.GetComponent<Rigidbody>().AddForce(exitForceVector+parentObject.GetComponent<Rigidbody>().velocity);
        }
    }

    public void fireSelectedObject()
    {
        if(selectedObject != null)
        {
            selectedObject.GetComponent<Rigidbody>().useGravity = true;
            float fireMult = fireForce * selectedObject.GetComponent<Rigidbody>().mass;
            Vector3 fireDirection = parentObject.transform.position + selectedObject.transform.position;
            selectedObject.GetComponent<Rigidbody>().AddForce(-transform.up*fireMult);
            selectedObject = null;
            selectedObjectPulledRotation = new Quaternion();
        }
    }

    public void turnBeamOff()
    {
        gameObject.SetActive(bool.Parse("False"));
    }

    public void turnBeamOn()
    {
        gameObject.SetActive(bool.Parse("True"));
    }
}
