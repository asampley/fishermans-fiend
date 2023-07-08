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
        //_fishingGame.Won += OnWon;
    }

    void OnDisable() {
        _fishingGame.Lost -= OnLost;
        //_fishingGame.Won -= OnWon;
    }

    public void Collide(Tentacle tentacle)
    {
        if (_isCollided == true) return;
        //Start Minigame
        Debug.Log("Collided");
        _isCollided = true;

        tentacle.Grab(this.gameObject);
        _parent.CalculateSpeed();
    }

    void OnLost()
    {
        _fishingGame.enabled = false;
        _parent.CalculateSpeed();
    }

    public bool IsRowing()
    {
        return !_fishingGame.enabled || _fishingGame.HasLost();
    }
}
