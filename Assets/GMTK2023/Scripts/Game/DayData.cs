using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DayData", menuName = "Scriptable Objects/DayData", order = 5)]
public class DayData : ScriptableObject
{
    public int DayNumber;
    public EnemyData[] EnemiesToSpawn;
    public ObstacleData[] ObstaclesToSpawn;
    public VictimData[] VictimToSpawn;
}
