using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Unit
{
    private ObstacleManager _obstacleManager;

    public Obstacle(ObstacleData data, bool spawnsOnLeft, float spawnHeight) : base(data, spawnsOnLeft, spawnHeight)
    {
        _obstacleManager = _transform.GetComponent<ObstacleManager>();

        _obstacleManager.Initialize(this, spawnsOnLeft);
    }
}
