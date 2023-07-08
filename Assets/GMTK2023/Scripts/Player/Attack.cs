using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField]
    private Transform _attackStartPos;
    [SerializeField]
    private GameObject _poisonDartPrefab;
    [SerializeField]
    private AbilityIconManager _abilityIconManager;

    private bool _isOnCooldown;
    private float _cooldownDuration;
    private float _timeElapsed;

    void OnEnable()
    {
        InputManager.Attack += _OnAttack;
    }

    void OnDisable()
    {
        InputManager.Attack -= _OnAttack;
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

    private void _OnAttack(InputManager.AttackEvent ev)
    {
        if (_isOnCooldown) return;

        GameObject g = Instantiate(_poisonDartPrefab, _attackStartPos);
        g.transform.position = _attackStartPos.position;
        g.GetComponent<ProjectileManager>().Initialize(ev.target, 10, 10f);

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
