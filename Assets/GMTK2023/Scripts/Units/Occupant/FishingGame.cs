using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingGame : MonoBehaviour
{
    public KeyCode key;

    public float resistance;
    public float health;

    public float opponentStrength;

    private float resistanceProgress;

    private bool _press;
    private bool press {
        get { return _press; }
        set { _press = value; ShouldPull?.Invoke(value); }
    }
    private float secondsToSwitch;

    public event Action<bool> ShouldPull;
    public event Action Won;
    public event Action Lost;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.HasWon() || this.HasLost())
        {
            return;
        }

        bool pressed = Input.GetKey(key);

        this.GetComponent<SpriteRenderer>().color = press ? Color.green : Color.red;

        if (press && pressed)
        {
            health -= opponentStrength * Time.fixedDeltaTime;
        }
        else if (press != pressed)
        {
            resistanceProgress += Time.fixedDeltaTime * resistance;
        }

        if (this.HasWon())
        {
            Won?.Invoke();
        }

        if (this.HasLost())
        {
            Lost?.Invoke();
        }

        secondsToSwitch -= Time.fixedDeltaTime;

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
