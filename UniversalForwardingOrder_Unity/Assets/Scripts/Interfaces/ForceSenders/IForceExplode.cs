using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForceExplode
{
    float explodeForce { get;}
    Vector3 explodeOrigin { get; }
    float explodeRadius { get;}
    float upwardsModifier { get;}

    void Explode();
}