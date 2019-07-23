using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICuttable:IDamagable
{
    void CutDamage(float damageTaken);
}
