using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkCloud : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _inkCloudSpriteRenderer;
    [SerializeField]
    private AbilityIconManager _abilityIconManager;

    private bool _isOnCooldown;
    private float _cooldownDuration;
    private float _timeElapsed;

    void OnEnable()
    {
        InputManager.InkCloud += _OnInkCloud;
    }

    void OnDisable()
    {
        InputManager.InkCloud -= _OnInkCloud;
    }


    private void Update()
    {
        if (!_isOnCooldown) return;

        _timeElapsed += Time.deltaTime;
        _abilityIconManager.SetMaskPercentage(_timeElapsed / _cooldownDuration);
        if (_timeElapsed >= _cooldownDuration)
        {
            _isOnCooldown = false;
        }
    }

    private void _OnInkCloud(InputManager.InkCloudEvent ev)
    {
        if (_isOnCooldown) return;

        

        _SetOnCooldown(GameManager.Instance.AttackCooldown);
    }

    private void _SetOnCooldown(float duration)
    {
        _timeElapsed = 0;
        _cooldownDuration = duration;
        _abilityIconManager.SetMaskPercentage(1);
        _isOnCooldown = true;
    }
}
