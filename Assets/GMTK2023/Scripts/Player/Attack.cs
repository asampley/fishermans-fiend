using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField]
    private Transform _attackStartPos;
    [SerializeField]
    private GameObject _poisonDartPrefab;

    void OnEnable()
    {
        InputManager.Attack += _OnAttack;
    }

    void OnDisable()
    {
        InputManager.Attack -= _OnAttack;
    }

    private void _OnAttack(InputManager.AttackEvent ev)
    {
        GameObject g = Instantiate(_poisonDartPrefab, _attackStartPos);
        g.transform.position = _attackStartPos.position;
        Debug.Log(ev.target);
        g.GetComponent<ProjectileManager>().Initialize(ev.target);
    }
}
