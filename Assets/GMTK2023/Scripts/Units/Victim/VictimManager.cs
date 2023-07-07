using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimManager : UnitManager, ICollidable
{
    [SerializeField] GameObject[] _occupants;

    public void Initialize(Victim victim, bool movesFromLeft, OccupantData[] occupantDatas)
    {
        int numOccupants = Random.Range(1, 4);

        for (int i = 0; i < numOccupants; i++)
        {
            _occupants[i].SetActive(true);
            OccupantManager occupantManager = _occupants[i].GetComponent<OccupantManager>();
            occupantManager.Initialize(occupantDatas[Random.Range(0, occupantDatas.Length)]);
        }

        base.Initialize(victim, movesFromLeft);
    }

    public void Collide(Tentacle tentacle)
    {

    }
}
