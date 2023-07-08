using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public AudioSource source { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            this.source = this.GetComponent<AudioSource>();
            _instance = this;
        }
    }
}
