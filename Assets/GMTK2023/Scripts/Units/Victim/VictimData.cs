using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VictimData", menuName = "Scriptable Objects/VictimData", order = 12)]
public class VictimData : UnitData
{
    public int VictimTypeId;
    public OccupantData[] PotentialOccupants;
}
