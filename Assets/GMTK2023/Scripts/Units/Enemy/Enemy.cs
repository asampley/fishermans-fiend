using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected int _attackDamage;
    public int AttackDamage => _attackDamage;

    protected EnemyManager _enemyManager;
    public Enemy(EnemyData data, bool spawnsOnLeft, float spawnHeight) : base(data, spawnsOnLeft, spawnHeight)
    {
        _attackDamage = data.AttackDamage;
        _enemyManager = _transform.GetComponent<EnemyManager>();

        _enemyManager.Initialize(this, spawnsOnLeft);
    }
}
