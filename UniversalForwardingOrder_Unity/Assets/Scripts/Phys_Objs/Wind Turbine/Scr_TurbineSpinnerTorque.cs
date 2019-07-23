using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TurbineSpinnerTorque : MonoBehaviour
{
    [Header("Turbine Force Stats")]
    public float torqueForce = 1000;
    float torqueMult;
    Vector3 torqueVector;


    float rigidbodyMass;

    private void OnEnable()
    {
        updateTorqueForce();
        spinRigidbody();
    }

    private void FixedUpdate()
    {
        updateTorqueForce();
        spinRigidbody();
    }

    void spinRigidbody()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(torqueVector);
    }

    void updateTorqueForce()
    {
        rigidbodyMass = gameObject.GetComponent<Rigidbody>().mass;
        torqueMult = torqueForce * rigidbodyMass;
        torqueVector = torqueMult * new Vector3(0, 0, 2);
    }
}
