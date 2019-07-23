using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scr_TUIOForceSender : MonoBehaviour
{
    [Header("Bashing Forces")]

    [Header("Exploding Forces")]
    public float explodeForce;
    public Vector3 explodeOrigin;
    public float explodeRadius;
    public float upwardModifier;
    public LayerMask explosionMask;

    private void OnEnable()
    {
        explodeOrigin = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        explodeOrigin = gameObject.transform.position;
    }

    public void Explode()
    {
        List<Collider> explosionList = Physics.OverlapSphere(transform.position, explodeRadius, explosionMask).ToList<Collider>();

        for (int i = 0; i < explosionList.Count; i++)//clean the list of null values
        {
            if (explosionList[i].GetComponent<IExplodable>() == null)
            {
                explosionList.RemoveAt(i);
            }
        }

        for (int i = 0; i < explosionList.Count; i++)//do the booms
        {
            if (explosionList[i].GetComponent<IExplodable>() != null)
            {
                explosionList[i].gameObject.GetComponent<Rigidbody>().AddExplosionForce(explodeForce, explodeOrigin, explodeRadius, upwardModifier);
            }
        }
        //Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
