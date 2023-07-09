using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGaugeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _powerGaugeObject;
    [SerializeField]
    private Image _powerGaugeMask;

    private bool _mouseIsDown = false;
    private Vector2 _startPos;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _MouseUp();
        }
        if (_mouseIsDown)
        {
            _WhileMouseDown();
        }
        if (Input.GetMouseButtonDown(0))
        {
            _MouseDown();
        }
    }

    private void _MouseDown()
    {
        _mouseIsDown = true;
        _powerGaugeObject.gameObject.SetActive(true);
        _startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void _WhileMouseDown()
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = ((_startPos - currentPos) * GameManager.Instance.TentacleVelocityScale);


        float fill = velocity.magnitude / GameManager.Instance.MaxTentacleLaunchStrength;
        _powerGaugeMask.fillAmount = fill;
    }

    private void _MouseUp()
    {
        _mouseIsDown = false;
        _powerGaugeObject.gameObject.SetActive(false);
        _powerGaugeMask.fillAmount = 0f;
    }
}
