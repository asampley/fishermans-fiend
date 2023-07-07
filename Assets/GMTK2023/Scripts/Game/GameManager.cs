using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentDay = 0;
    public int CurrentDay => _currentDay;
    private DayData _currentDayData;

    private void Start()
    {
        _selectNextDay();
    }

    private void FixedUpdate()
    {
        
    }


    private void _selectNextDay()
    {
        _currentDay++;
        _currentDayData = Globals.DAY_DATA.Where((DayData x) => x.DayNumber == _currentDay).First();
    }

    private void _SpawnUnit(UnitData data)
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;

        if (data.SpawnsBelowSurface)
        {
            float height = Random.Range(Globals.Y_MIN, Globals.Y_MAX);
            new Victim(data, spawnOnLeft, height);
        }
        else
        {
            new Victim(data, spawnOnLeft, Globals.SURFACE_HEIGHT);
        }
        
    }
}
