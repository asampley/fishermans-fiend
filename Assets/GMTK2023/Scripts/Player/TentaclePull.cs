using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePull : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();

        Vector3[] positions = new Vector3[line.positionCount];
        line.GetPositions(positions);

        float deltaX = positions[^1].x - positions[0].x;
        float deltaY = positions[^1].y - positions[0].y;

        for (int i = 1; i < positions.Length - 1; ++i) {
            Vector2 target = new
                (
                    deltaX * i / (positions.Length - 1),
                    deltaY * Mathf.Pow((float)i / (positions.Length - 1), 2f)
                );

            positions[i] = Vector3.Lerp(positions[i], target, Time.fixedDeltaTime);
        }

        line.SetPositions(positions);
    }
}
