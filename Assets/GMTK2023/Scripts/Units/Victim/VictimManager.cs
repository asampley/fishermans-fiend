using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictimManager : UnitManager, ICollidable
{
    [SerializeField] GameObject[] _occupants;

    public void Initialize(Victim victim, bool movesFromLeft, OccupantData[] occupantDatas)
    {
        int numOccupants = UnityEngine.Random.Range(1, 4);

        for (int i = 0; i < numOccupants; i++)
        {
            _occupants[i].SetActive(true);
            OccupantManager occupantManager = _occupants[i].GetComponent<OccupantManager>();
            occupantManager.Initialize(occupantDatas[UnityEngine.Random.Range(0, occupantDatas.Length)], this);
        }

        Array.Resize(ref _occupants, numOccupants);

        base.Initialize(victim, movesFromLeft);
    }

    public void Collide(Tentacle tentacle)
    {

    }

    public void CalculateSpeed()
    {
        int rowing = this._occupants.Where(o => o.GetComponent<OccupantManager>().IsRowing()).Count();
        Debug.Log("Speed: " + rowing + "/" + _occupants.Length);
        this.speedFactor = (float)rowing / _occupants.Length;
    }
}
