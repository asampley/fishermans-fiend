using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSet
{
    [SerializeField]
    AudioClip[] audios;

    public AudioClip Rand() => audios[Random.Range(0, audios.Length)];
}
