using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePull : MonoBehaviour
{
    public GameObject target;

    void OnEnable()
    {
        var tentacle = this.GetComponent<Tentacle>();

        var game = target.GetComponent<FishingGame>();
        game.opponentStrength = tentacle.strength;
        game.key = KeyCode.Alpha1;
        game.Lost += tentacle.Fall;
        game.enabled = true;
    }

    void OnDisable()
    {
        var tentacle = this.GetComponent<Tentacle>();

        var game = target.GetComponent<FishingGame>();
        game.Lost -= tentacle.Fall;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();

        Vector3[] positions = new Vector3[line.positionCount];
        line.GetPositions(positions);

        positions[^1] = this.target.GetComponent<Transform>().position
            + (Vector3)this.target.GetComponent<Collider2D>().offset
            - this.transform.position;

        float deltaX = positions[^1].x - positions[0].x;
        float deltaY = positions[^1].y - positions[0].y;

        for (int i = 1; i < positions.Length - 1; ++i) {
            Vector2 positionTarget = new
                (
                    Mathf.Lerp(positions[i - 1].x, positions[i + 1].x, 0.5f),
                    Mathf.Lerp(positions[i - 1].y, positions[i + 1].y, 0.45f)
                );

            positions[i] = positionTarget;
        }

        line.SetPositions(positions);
    }
}
