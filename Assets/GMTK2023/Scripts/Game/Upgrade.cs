using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    private UpgradeEffect _effect;
    public UpgradeEffect Effect => _effect;
    private float _amount;
    public float Amount => _amount;
    private int _cost;
    public int Cost => _cost;

    public Upgrade(UpgradeEffect effect, float amount, int cost)
    {
        _effect = effect;
        _amount = amount;
        _cost = cost;
    }
}
