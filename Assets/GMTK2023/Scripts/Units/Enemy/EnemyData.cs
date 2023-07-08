using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData", order = 10)]
public class EnemyData : UnitData
{
    public int EnemyTypeId;
    public int AttackDamage;
}
