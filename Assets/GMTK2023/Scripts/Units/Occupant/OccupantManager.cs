using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupantManager : MonoBehaviour, ICollidable
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Collider2D _collider;
    private bool _isCollided = false;
    private VictimManager _parent;

    public void Initialize(OccupantData data, VictimManager parent)
    {
        _spriteRenderer.sprite = data.Sprite;
        _parent = parent;
    }

    public void Collide(Tentacle tentacle)
    {
        if (_isCollided == true) return;
        //Start Minigame
        Debug.Log("Collided");
        _isCollided = true;

        tentacle.Grab(this.gameObject);
        _parent.Stop();

        var game = this.GetComponent<FishingGame>();
        game.opponentStrength = tentacle.strength;
        game.key = KeyCode.Alpha1;
        game.enabled = true;
    }
}
