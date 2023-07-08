using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingGame : MonoBehaviour
{
    private static bool[] takenKeys = new bool[Globals.KEYS.Length];

    private int key;
    private KeyCode keyCode { get { return Globals.KEYS[key]; } }

    public float resistance;
    public float health;

    public Tentacle tentacle { get; set; }

    private float resistanceProgress;

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
            health -= GameManager.Instance.TentacleStrength * Time.deltaTime;
        }
        else if (press != pressed)
        {
            resistanceProgress += Time.deltaTime * resistance;
        }

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
        return resistanceProgress >= 1.0;
    }

    public bool HasWon()
    {
        return health <= 0.0;
    }
}
