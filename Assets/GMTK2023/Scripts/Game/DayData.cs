using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DayData", menuName = "Scriptable Objects/DayData", order = 5)]
public class DayData : ScriptableObject
{
    public int DayNumber;
    public EnemyData[] DayEnemiesToSpawn;
    public ObstacleData[] DayObstaclesToSpawn;
    public VictimData[] DayVictimsToSpawn;

    public EnemyData[] NightEnemiesToSpawn;
    public ObstacleData[] NightObstaclesToSpawn;
    public VictimData[] NightVictimsToSpawn;

    public float SpawnCooldown;
    public float SpawnEnemyTimeMuliplier = 1f;
    public float SpawnObstacleTimeMultiplier = 1f;

    public int RequiredBiomass;
}
