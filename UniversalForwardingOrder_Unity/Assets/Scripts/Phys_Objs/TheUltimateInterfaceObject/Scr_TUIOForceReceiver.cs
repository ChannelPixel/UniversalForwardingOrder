using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TUIOForceReceiver : MonoBehaviour, IBashable, ICuttable, IExplodable, IStabable, ITractorable
{
    void ICuttable.CutDamage(float damageTaken)
    {

    }

    void IDamagable.Damage(float damageTaken)
    {

    }
}
