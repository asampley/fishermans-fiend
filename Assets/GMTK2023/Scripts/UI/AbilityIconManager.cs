using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconManager : MonoBehaviour
{
    [SerializeField]
    private Image _cooldownMask;

    private bool _isOnCooldown;
    private float _cooldownDuration;
    private float _timeElapsed;




    public void SetMaskPercentage(float percentage)
    {
        _cooldownMask.fillAmount = 1 - percentage;
    }
}
