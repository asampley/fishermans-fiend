using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupantManager : MonoBehaviour, ICollidable
{
    private OccupantData data;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;
    [SerializeField] FishingGame _fishingGame;
    private bool _isCollided = false;
    private VictimManager _parent;

    public event Action<OccupantManager> Fall;

    public void Initialize(OccupantData data, VictimManager parent)
    {
        this.data = data;

        _spriteRenderer.sprite = data.Sprite;
        _parent = parent;
        _fishingGame.resistance = data.Resistance;
        _fishingGame.health = data.Health;
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

    void OnLost()
    {
        _parent.CalculateSpeed();
        AudioManager.Instance?.source.PlayOneShot(data.fall.Rand());
    }

    void OnWon()
    {
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
