using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private void Awake()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        Globals.ENEMY_DATA = Resources.LoadAll<EnemyData>(Globals.ENEMY_DATA_FOLDER);
        Globals.OBSTACLE_DATA = Resources.LoadAll<ObstacleData>(Globals.OBSTACLE_DATA_FOLDER);
        Globals.VICTIM_DATA = Resources.LoadAll<VictimData>(Globals.VICTIM_DATA_FOLDER);
    }
}
