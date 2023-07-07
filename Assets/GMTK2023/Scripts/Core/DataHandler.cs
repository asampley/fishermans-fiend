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
        Globals.ENEMY_DATA = Resources.LoadAll<UnitData>(Globals.ENEMY_DATA_FOLDER);
        Globals.OBSTACLE_DATA = Resources.LoadAll<UnitData>(Globals.OBSTACLE_DATA_FOLDER);
        Globals.VICTIM_DATA = Resources.LoadAll<UnitData>(Globals.VICTIM_DATA_FOLDER);
    }
}
