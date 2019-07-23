using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TurbineSpinnerForceSender : MonoBehaviour, IForceCut
{
    [Header("Forces Stats")]
    public float cutForce = 1f;

    float IForceCut.cutForce
    {
        get { return cutForce; }
    }
}
