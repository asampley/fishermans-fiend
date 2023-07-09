using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingGame : MonoBehaviour
{
    private static bool[] takenKeys = new bool[Globals.KEYS.Length];

    public AudioClip clip;
    public AudioSource source;

    public Sprite pressPressed;
    public Sprite nopressPressed;
    public Sprite pressNotpressed;
    public Sprite nopressNotpressed;

    public Transform progressBar;

    private int key;
    private KeyCode keyCode { get { return Globals.KEYS[key]; } }

    public float resistance;
    public float progressRate;

    public Tentacle tentacle { get; set; }

    private float resistanceProgress;
    private float progress = 0.5f;

    private bool _press;
    private bool press
    {
        get { return _press; }
        set { _press = value; ShouldPull?.Invoke(value); }
    }
    private float secondsToSwitch;

    public SpriteRenderer gameImage;
    public TextMeshPro text;

    public event Action<bool> ShouldPull;
    public event Action Won;
    public event Action Lost;

    void OnEnable()
    {
        source.clip = clip;
        source.Play();

        for (int i = 0; i < takenKeys.Length; ++i)
        {
            if (!takenKeys[i])
            {
                takenKeys[i] = true;
                this.key = i;
                this.text.text = KeyToString(keyCode);
                gameImage.gameObject.SetActive(true);
                return;
            }
        }

        // if we can't find a key throw an exception
        throw new Exception("Unable to get a key for tentacle");
    }

    void OnDisable()
    {
        source.clip = null;
        source.Stop();

        takenKeys[this.key] = false;
        gameImage.gameObject.SetActive(false);
    }

    public static string KeyToString(KeyCode key)
    {
        return key.ToString().Replace("Alpha", null);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.HasWon() || this.HasLost())
        {
            this.enabled = false;
            return;
        }

        bool pressed = Input.GetKey(keyCode);

        gameImage.color = press ? Color.green : Color.red;

        if (press && pressed)
        {
            gameImage.sprite = pressPressed;
            progress += progressRate * GameManager.Instance.TentacleStrength * Time.deltaTime;
        }
        else if (press != pressed)
        {
            gameImage.sprite = press ? pressNotpressed : nopressPressed;
            progress -= Time.deltaTime * resistance;
        }
        else
        {
            gameImage.sprite = nopressNotpressed;
        }

        progressBar.localScale = new(progress, 1f, 1f);

        if (this.HasWon())
        {
            Won?.Invoke();
        }

        if (this.HasLost())
        {
            Lost?.Invoke();
        }

        secondsToSwitch -= Time.deltaTime;

        if (secondsToSwitch <= 0)
        {
            press = !press;
            secondsToSwitch = UnityEngine.Random.Range(1f, 2f);
        }
    }

    public bool HasLost()
    {
        return progress <= 0.0;
    }

    public bool HasWon()
    {
        return progress >= 1.0;
    }
}
