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
        if (!_isOnCooldown || !GameManager.Instance.CanInkCloud) return;

        _timeElapsed += Time.deltaTime;
        _abilityIconManager.SetMaskPercentage(_timeElapsed / _cooldownDuration);
        if (_timeElapsed >= _cooldownDuration)
        {
            _isOnCooldown = false;
            GameManager.Instance.SetInkCloudActive(false);
        }
    }

    private void _OnInkCloud(InputManager.InkCloudEvent ev)
    {
        if (_isOnCooldown || !GameManager.Instance.CanInkCloud) return;

        GameManager.Instance.SetInkCloudActive(true);
        StartCoroutine(_FadeInkCloud(5f));

        _SetOnCooldown(GameManager.Instance.InkCloudCooldown);
    }

    private void _SetOnCooldown(float duration)
    {
        _timeElapsed = 0;
        _cooldownDuration = duration;
        _abilityIconManager.SetMaskPercentage(1);
        _isOnCooldown = true;
    }

    private IEnumerator _FadeInkCloud(float duration)
    {
        Debug.Log("Running");
        Color full = new(1, 1, 1, 1);
        Color hidden = new(1, 1, 1, 0);

        float ElapsedTime = 0.0f;
        float TotalTime = duration;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            _inkCloudSpriteRenderer.color = Color.Lerp(full, hidden, (ElapsedTime / TotalTime));
            yield return null;
        }

        yield return null;
    }

    public void ActivateManager()
    {
        _abilityIconManager.gameObject.SetActive(true);
    }
}
