using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private Unit _unit;
    private bool _movesFromLeft;
    public float speedFactor = 1f;
    private int _health;
    private bool _initialized = false;

    public void Initialize(Unit unit, bool movesFromLeft)
    {
        _unit = unit;
        _health = unit.Health;
        _movesFromLeft = movesFromLeft;
        _spriteRenderer.sprite = unit.Sprite;
        _spriteRenderer.flipX = !movesFromLeft;
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized || Mathf.Abs(speedFactor) <= 1e-6) return;

        Vector2 direction = _movesFromLeft ? Vector2.right : Vector2.left;

        this.transform.Translate(this.speedFactor * _unit.Speed * Time.deltaTime * direction);

        if (this.transform.position.x > Globals.X_EDGE || this.transform.position.x < -Globals.X_EDGE)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    protected void _Damage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            _Die();
        }
    }

    protected void _Die()
    {
        Destroy(this.gameObject);
    }
}
