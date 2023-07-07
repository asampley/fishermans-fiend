using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private Transform _transform;
    private UnitManager _unitManager;

    private Sprite _sprite;
    public Sprite Sprite => _sprite;
    private float _speed;
    public float Speed => _speed;

    public Unit(UnitData data, bool spawnsOnLeft, float spawnHeight)
    {
        GameObject g = GameObject.Instantiate(data.Prefab) as GameObject;
        _transform = g.transform;
        _transform.position = spawnsOnLeft ? new(-Globals.X_EDGE, spawnHeight): new(Globals.X_EDGE, spawnHeight);
        _unitManager = _transform.GetComponent<UnitManager>();
        
        _sprite = data.Sprite;
        _speed = data.Speed;

        _unitManager.Initialize(this, spawnsOnLeft);
    }
}
