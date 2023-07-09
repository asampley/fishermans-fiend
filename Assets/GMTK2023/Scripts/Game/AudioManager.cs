using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public AudioClip gameMusic;
    public AudioClip menuMusic;

    public AudioSource source { get; private set; }

    void Start()
    {
        GameManager.Instance.NewDay += OnNewDay;
        GameManager.Instance.FinishDay += OnFinishDay;
        GameManager.Instance.PauseGame += OnPause;
    }

    void OnDestroy()
    {
        GameManager.Instance.NewDay -= OnNewDay;
        GameManager.Instance.FinishDay -= OnFinishDay;
        GameManager.Instance.PauseGame -= OnPause;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            this.source = this.GetComponent<AudioSource>();
            _instance = this;
        }
    }

    void OnNewDay()
    {
        source.clip = gameMusic;
        if (!GameManager.Instance.GameIsPaused)
        {
            source.Play();
        }
    }

    void OnFinishDay()
    {
        source.clip = menuMusic;
        source.Play();
    }

    void OnPause(bool paused)
    {
        Debug.Log(source.clip == gameMusic);
        if (source.clip == gameMusic && paused)
        {
            source.Pause();
        } else {
            source.UnPause();
        }
    }
}
