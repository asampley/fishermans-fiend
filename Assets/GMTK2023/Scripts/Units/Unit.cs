using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    protected Transform _transform;

    protected Sprite _sprite;
    public Sprite Sprite => _sprite;
    protected int _health;
    public int Health => _health;
    protected float _speed;
    public float Speed => _speed;

    public Unit(UnitData data, bool spawnsOnLeft, float spawnHeight)
    {
        GameObject g = GameObject.Instantiate(data.Prefab) as GameObject;
        _transform = g.transform;
        _transform.position = spawnsOnLeft ? new(-Globals.X_EDGE, spawnHeight): new(Globals.X_EDGE, spawnHeight);

        
        _sprite = data.Sprite;
        _health = data.Health;
        _speed = data.Speed;
    }
}
