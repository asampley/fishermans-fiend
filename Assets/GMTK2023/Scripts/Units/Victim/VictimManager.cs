using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictimManager : UnitManager, ICollidable
{
    [SerializeField] List<GameObject> _occupants;

    public event Action<VictimManager> Capsize;

    public void Initialize(Victim victim, bool movesFromLeft, OccupantData[] occupantDatas)
    {
        int numOccupants = UnityEngine.Random.Range(1, 4);

        for (int i = 0; i < numOccupants; i++)
        {
            _occupants[i].SetActive(true);
            OccupantManager occupantManager = _occupants[i].GetComponent<OccupantManager>();
            occupantManager.Initialize(occupantDatas[UnityEngine.Random.Range(0, occupantDatas.Length)], this);
            occupantManager.Fall += this.RemoveOccupant;
        }

        _occupants.RemoveRange(numOccupants, _occupants.Count - numOccupants);

        base.Initialize(victim, movesFromLeft);

        this.Capsize += OnCapsize;
    }

    public void OnDestroy()
    {
        foreach (GameObject occupant in _occupants)
        {
            occupant.GetComponent<OccupantManager>().Fall -= this.RemoveOccupant;
        }
    }

    public void Collide(Tentacle tentacle)
    {

    }

    public void Collide(ProjectileManager projectile)
    {

    }

    public void CalculateSpeed()
    {
        int rowing = this._occupants.Where(o => o.GetComponent<OccupantManager>().IsRowing()).Count();
        this.speedFactor = (float)rowing / Mathf.Max(_occupants.Count, 1);
    }

    public void RemoveOccupant(OccupantManager occupant)
    {
        _occupants.Remove(occupant.gameObject);
        this.CalculateSpeed();
        this.CheckCapsize();
    }

    public void CheckCapsize()
    {
        if (_occupants.Count == 0)
        {
            Capsize?.Invoke(this);
        }
    }

    static void OnCapsize(VictimManager manager)
    {
        manager.GetComponent<SpriteRenderer>().flipY = true;
        manager.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
