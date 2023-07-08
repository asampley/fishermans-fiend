using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupantManager : MonoBehaviour, ICollidable
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;
    [SerializeField] FishingGame _fishingGame;
    private bool _isCollided = false;
    private VictimManager _parent;

    public void Initialize(OccupantData data, VictimManager parent)
    {
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

    public void Collide(ProjectileManager projectile)
    {

    }

    void OnLost()
    {
        _parent.CalculateSpeed();
    }

    void OnWon()
    {
        this.Fall();
    }

    public bool IsRowing()
    {
        return !_fishingGame.enabled || _fishingGame.HasLost();
    }

    public void Fall()
    {
        Debug.Log("Falling");
        this.transform.SetParent(_fishingGame.tentacle.transform);
    }
}
