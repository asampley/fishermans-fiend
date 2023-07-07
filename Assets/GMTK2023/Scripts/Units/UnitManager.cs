using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private Unit _unit;
    private bool _movesFromLeft;

    public void Initialize(Unit unit, bool movesFromLeft)
    {
        _unit = unit;
        _movesFromLeft = movesFromLeft;
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
    }
}
