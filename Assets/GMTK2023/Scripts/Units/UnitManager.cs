using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private Unit _unit;
    private bool _movesFromLeft;

    public void Initialize(Unit unit, bool movesFromLeft)
    {
        _unit = unit;
        _movesFromLeft = movesFromLeft;
        _spriteRenderer.sprite = unit.Sprite;
    }

    private void Update()
    {
        if (_movesFromLeft)
        {
            this.transform.Translate(_unit.Speed * Time.deltaTime * Vector2.right);
        }
        else
        {
            this.transform.Translate(_unit.Speed * Time.deltaTime * Vector2.left);
        }

        if (this.transform.position.x > Globals.X_EDGE || this.transform.position.x < -Globals.X_EDGE)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
