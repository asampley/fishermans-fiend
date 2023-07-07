using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        int unitId = 0;
        VictimData unitToSpawn = Globals.VICTIM_DATA.Where((VictimData x) => x.VictimTypeId == unitId).First();
        new Victim(unitToSpawn, false, Globals.SURFACE_HEIGHT);
    }
}
