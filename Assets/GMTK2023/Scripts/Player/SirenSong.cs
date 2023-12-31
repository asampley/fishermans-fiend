using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenSong : MonoBehaviour
{
    [SerializeField]
    private AbilityIconManager _abilityIconManager;

    private bool _isOnCooldown;
    private float _cooldownDuration;
    private float _timeElapsed;

    [SerializeField]
    private AudioClip _sirenSongClip;

    void OnEnable()
    {
        InputManager.SirenSong += _OnSirenSong;
    }

    void OnDisable()
    {
        InputManager.SirenSong -= _OnSirenSong;
    }


    private void Update()
    {
        if (!_isOnCooldown || !GameManager.Instance.CanSirenSong) return;

        _timeElapsed += Time.deltaTime;
        _abilityIconManager.SetMaskPercentage(_timeElapsed / _cooldownDuration);
        if (_timeElapsed > GameManager.Instance.SirenSongDuration)
        {
            GameManager.Instance.SetSirenSongActive(false);
        }
        if (_timeElapsed >= _cooldownDuration)
        {
            _isOnCooldown = false;
        }
    }

    private void _OnSirenSong(InputManager.SirenSongEvent ev)
    {
        if (_isOnCooldown || !GameManager.Instance.CanSirenSong) return;

        GameManager.Instance.SetSirenSongActive(true);
        _SetOnCooldown(GameManager.Instance.SirenSongCooldown);
        AudioManager.Instance.source.PlayOneShot(_sirenSongClip);
    }

    private void _SetOnCooldown(float duration)
    {
        _timeElapsed = 0;
        _cooldownDuration = duration;
        _abilityIconManager.SetMaskPercentage(1);
        _isOnCooldown = true;
    }

    public void ActivateManager()
    {
        _abilityIconManager.gameObject.SetActive(true);
    }
}
