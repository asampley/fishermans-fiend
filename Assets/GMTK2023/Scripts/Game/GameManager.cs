using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        int unitId = 0;
        UnitData unitToSpawn = Globals.VICTIM_DATA.Where((UnitData x) => x.UnitTypeId == unitId).First();
        new Unit(unitToSpawn, false, Globals.SURFACE_HEIGHT);
    }
}
