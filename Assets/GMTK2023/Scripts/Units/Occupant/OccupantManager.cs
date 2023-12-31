using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupantManager : MonoBehaviour, ICollidable
{
    public OccupantData data { get; private set; }

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;
    [SerializeField] FishingGame _fishingGame;
    [SerializeField] SpriteRenderer _fishingRod;
    private bool _isCollided = false;
    private VictimManager _parent;

    public event Action<OccupantManager> Fall;

    public void Initialize(OccupantData data, VictimManager parent)
    {
        this.data = data;

        _spriteRenderer.sprite = data.Sprite;
        _parent = parent;
        _fishingGame.resistance = data.Resistance;
        _fishingGame.progressRate = data.ProgressRate;
        _fishingGame.nopressTimeRange = data.StopTimeRange;
        _fishingGame.pressTimeRange = data.GoTimeRange;
        _fishingGame.clip = data.fighting.Rand();
    }

    void OnEnable() {
        _fishingGame.Lost += OnLost;
        _fishingGame.Won += OnWon;
    }

    void OnDisable() {
        _fishingGame.Lost -= OnLost;
        _fishingGame.Won -= OnWon;
    }

    public void Collide(Tentacle tentacle)
    {
        if (_isCollided == true) return;
        //Start Minigame
        _isCollided = true;

        tentacle.Grab(this.gameObject);
        _parent.CalculateSpeed();
    }

    public void Collide(ProjectileManager projectile)
    {

    }

    void OnLost()
    {
        _parent.CalculateSpeed();
        _fishingRod.enabled = false;
        AudioManager.Instance?.source.PlayOneShot(data.fall.Rand());
    }

    void OnWon()
    {
        EventManager.TriggerEvent("CaughtOccupant", this);
        GameManager.Instance.PlayerDefeatOccupant(this);
        this.transform.SetParent(_fishingGame.tentacle.transform);
        this.Fall?.Invoke(this);
        AudioManager.Instance?.source.PlayOneShot(data.fall.Rand());
        AudioManager.Instance?.source.PlayOneShot(data.falling.Rand());
    }

    public bool IsRowing()
    {
        return !_fishingGame.enabled || _fishingGame.HasLost();
    }
}
