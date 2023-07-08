using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : Unit
{
    private VictimManager _victimManager;


    public Victim(VictimData data, bool spawnsOnLeft, float spawnHeight) : base(data, spawnsOnLeft, spawnHeight)
    {
        _victimManager = _transform.GetComponent<VictimManager>();

        _victimManager.Initialize(this, data, spawnsOnLeft, data.PotentialOccupants);
    }
}
