using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SandwichIngredientBaseForceReceiver : MonoBehaviour, IHealth, ITractorable, ICuttable
{
    [Header("Health Stats")]
    public float health = 2f;
    public float cuttableHealth = 1f;

    [Header("ICuttable Death Stats")]
    public bool doItemsSpawnOnDeath = true;
    public GameObject itemSpawned;
    public float itemSpawnAmount = 1;

    float IHealth.health
    {
        get { return health; }
        set { health = value; }
    }

    void IDamagable.Damage(float damageTaken)
    {
        health -= damageTaken;
        checkHealth();
    }

    void ICuttable.CutDamage(float damageTaken)
    {
        cuttableHealth -= damageTaken;
        this.GetComponent<IDamagable>().Damage(damageTaken);
    }

    void checkHealth()
    {
        //Check health heirarchy changes depending on the IRL object's logic. All force healths have to be checked first before the object's main health.
        if(cuttableHealth <= 0)
        {
            CutKill();
        }
        else if(health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    //Kill was by a cutting force
    void CutKill()
    {
        if(doItemsSpawnOnDeath && itemSpawned != null)
        {
            
            //Spawn items at death location/rotation.
            float a = 0;
            while (a < itemSpawnAmount)
            {
                Instantiate(itemSpawned, gameObject.transform.position, Random.rotation);
                a++;
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Have we collided with a cuttable force?
        if(collision.collider.GetComponent<IForceCut>() != null)
        {
            float cutForceReceived = collision.collider.GetComponent<IForceCut>().cutForce;
            this.GetComponent<ICuttable>().CutDamage(cutForceReceived);
        }
    }
}
