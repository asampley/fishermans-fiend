using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : UnitManager, ICollidable
{
    private Enemy _enemy;

    public void Initialize(Enemy enemy, bool movesFromLeft)
    {
        _enemy = enemy;

        base.Initialize(enemy, movesFromLeft);
    }

    public void Collide(Tentacle tentacle)
    {
        tentacle.Blocked();
        GameManager.Instance.PlayerTakeDamage(_enemy.AttackDamage);
    }

    public void Collide(ProjectileManager projectile)
    {
        _Damage(projectile.AttackDamage);
    }
}
