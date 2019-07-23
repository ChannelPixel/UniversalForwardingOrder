using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BreadSliceSandwichAssemble : MonoBehaviour
{
    [Header("Ingredients")]
    public GameObject objectNameBreadSlice;
    public GameObject breadSliceIngredient = null;
    public GameObject objectNameTomatoSlice;
    public GameObject tomatoSliceIngredient = null;
    public GameObject objectNameLeafyGreen;
    public GameObject leafyGreenIngredient = null;
    public GameObject objectNameCheeseSlice;
    public GameObject cheeseSliceIngredient = null;
    public GameObject objectNameBaconSlice;
    public GameObject baconSliceIngredient = null;
    [Header("Assembly spawn info")]
    public GameObject itemSpawned;

    private void FixedUpdate()
    {
        if(breadSliceIngredient != null
            &&tomatoSliceIngredient != null
            &&leafyGreenIngredient != null
            &&cheeseSliceIngredient != null
            &&baconSliceIngredient != null)
        {
            constructSandwich();
        }
    }

    void constructSandwich()
    {
        Destroy(breadSliceIngredient);
        Destroy(tomatoSliceIngredient);
        Destroy(leafyGreenIngredient);
        Destroy(cheeseSliceIngredient);
        Destroy(baconSliceIngredient);
        Instantiate(itemSpawned, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Scr_SandwichRefinedIngredientForceReceiver>() != null)
        {
            string objectName = other.gameObject.name;
            if(objectName.Contains(objectNameBaconSlice.name))
            {
                baconSliceIngredient = other.gameObject;
            }
            else if (objectName.Contains(objectNameCheeseSlice.name))
            {
                cheeseSliceIngredient = other.gameObject;
            }
            else if (objectName.Contains(objectNameLeafyGreen.name))
            {
                leafyGreenIngredient = other.gameObject;
            }
            else if (objectName.Contains(objectNameTomatoSlice.name))
            {
                tomatoSliceIngredient = other.gameObject;
            }
            else if (objectName.Contains(objectNameBreadSlice.name) && other.gameObject != gameObject)
            {
                breadSliceIngredient = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Scr_SandwichRefinedIngredientForceReceiver>() != null)
        {
            if (other.gameObject == baconSliceIngredient)
            {
                baconSliceIngredient = null;
            }
            else if (other.gameObject == cheeseSliceIngredient)
            {
                cheeseSliceIngredient = null;
            }
            else if (other.gameObject == leafyGreenIngredient)
            {
                leafyGreenIngredient = null;
            }
            else if (other.gameObject == tomatoSliceIngredient)
            {
                tomatoSliceIngredient = null;
            }
            else if (other.gameObject == breadSliceIngredient)
            {
                breadSliceIngredient = null;
            }
        }
    }
}
