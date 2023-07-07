using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupantManager : MonoBehaviour, ICollidable
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;

    public void Initialize(OccupantData data)
    {
        _spriteRenderer.sprite = data.Sprite;

    }

    public void Collide(Tentacle tentacle)
    {
        //Start Minigame
        Debug.Log("Collided");
    }
}
