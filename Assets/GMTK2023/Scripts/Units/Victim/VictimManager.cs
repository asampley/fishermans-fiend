using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimManager : UnitManager, ICollidable
{
    [SerializeField] public Collider2D[] _colliders;
    private float _numPassengers;

    public void Initialize()
    {
        
    }

    public void Collide()
    {

    }
}
