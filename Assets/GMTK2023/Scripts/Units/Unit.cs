using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private Transform _transform;
    private UnitManager _unitManager;

    private float _speed;
    public float Speed => _speed;

    public Unit(UnitData data, bool spawnsOnLeft, float spawnHeight)
    {
        GameObject g = GameObject.Instantiate(data.Prefab) as GameObject;
        _transform = g.transform;
        _transform.position = spawnsOnLeft ? new(-Globals.X_EDGE, spawnHeight): new(Globals.X_EDGE, spawnHeight);
        _unitManager = _transform.GetComponent<UnitManager>();
        _unitManager.Initialize(this, spawnsOnLeft);

        _speed = data.Speed;
    }
}
