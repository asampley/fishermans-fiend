using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictimManager : UnitManager, ICollidable
{
    [SerializeField] List<GameObject> _occupants;
    private bool _hasSeenHorror;

    private VictimData data;

    public event Action<VictimManager> Capsize;

    public void Initialize(Victim victim, VictimData data, bool movesFromLeft, OccupantData[] occupantDatas)
    {
        this.data = data;

        int numOccupants = UnityEngine.Random.Range(1, _occupants.Count);

        for (int i = 0; i < numOccupants; i++)
        {
            _occupants[i].SetActive(true);
            OccupantManager occupantManager = _occupants[i].GetComponent<OccupantManager>();
            occupantManager.Initialize(SelectOccupantFromData(occupantDatas), this);
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
            if (_hasSeenHorror)
            {
                EventManager.TriggerEvent("IncreaseAwareness", 5);
            }
            occupant.GetComponent<OccupantManager>().Fall -= this.RemoveOccupant;
        }
    }

    public void Collide(Tentacle tentacle)
    {
        tentacle.Blocked();
        EventManager.TriggerEvent("IncreaseAwareness", 1);
    }

    public void Collide(ProjectileManager projectile)
    {
        EventManager.TriggerEvent("IncreaseAwareness", 5);
    }

    public void CalculateSpeed()
    {
        int rowing = this._occupants.Where(o => o.GetComponent<OccupantManager>().IsRowing()).Count();
        this.speedFactor = 0.5f * (1f + (float)rowing / Mathf.Max(_occupants.Count, 1));
    }

    public void RemoveOccupant(OccupantManager occupant)
    {
        _occupants.Remove(occupant.gameObject);
        this.CalculateSpeed();
        this.CheckCapsize();
        _hasSeenHorror = true;
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
        AudioManager.Instance.source.PlayOneShot(manager.data.falling.Rand());
        AudioManager.Instance.source.PlayOneShot(manager.data.fall.Rand());
        manager.GetComponent<Collider2D>().enabled = false;
    }

    OccupantData SelectOccupantFromData(OccupantData[] datas)
    {
        float rarityRange = datas.Select(d => 1f / d.Rarity).Sum();
        float selector = UnityEngine.Random.Range(0f, rarityRange);

        foreach (OccupantData data in datas)
        {
            selector -= 1f / data.Rarity;
            if (selector <= 0.0) {
                return data;
            }
        }

        return datas[^1];
    }
}
