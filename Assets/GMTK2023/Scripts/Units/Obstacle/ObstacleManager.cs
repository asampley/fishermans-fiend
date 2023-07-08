using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : UnitManager, ICollidable
{
    public void Collide(Tentacle tentacle)
    {

    }
    public void Collide(ProjectileManager projectile)
    {
        _Damage(projectile.AttackDamage);
    }
}
