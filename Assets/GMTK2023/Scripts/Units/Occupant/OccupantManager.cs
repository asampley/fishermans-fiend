using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupantManager : MonoBehaviour, ICollidable
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;
    private bool _isCollided = false;

    public void Initialize(OccupantData data)
    {
        _spriteRenderer.sprite = data.Sprite;
    }

    public void Collide(Tentacle tentacle)
    {
        if (_isCollided == true) return;
        //Start Minigame
        Debug.Log("Collided");
        _isCollided = true;

        tentacle.Grab(this.gameObject);
    }
}
