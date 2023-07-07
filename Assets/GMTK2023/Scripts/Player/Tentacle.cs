#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [SerializeField]
    private Vector2 position = Vector2.zero;

    private Vector3[] positionBuffer = new Vector3[1];

    // peak to determine when to despawn
    public float peakY { get; private set; } = 0f;

    public Vector2 velocity = Vector2.zero;
    public Vector2 acceleration = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.velocity += this.acceleration * Time.fixedDeltaTime;
        this.position += this.velocity * Time.fixedDeltaTime;

        this.peakY = Math.Max(this.peakY, this.position.y);

        if (this.transform.localPosition.y + this.peakY < 0) {
            Destroy(this.gameObject);
        }

        AddLinePosition(this.position);
        UpdateColliderPosition();
    }

    void AddLinePosition(Vector3 add)
    {
        LineRenderer line = this.GetComponent<LineRenderer>();

        if (this.positionBuffer.Length < line.positionCount + 1)
        {
            Array.Resize(ref this.positionBuffer, Math.Max(1, this.positionBuffer.Length * 2));
        }

        this.positionBuffer[line.positionCount] = add;

        line.positionCount += 1;
        line.SetPositions(this.positionBuffer);
    }

    void UpdateColliderPosition()
    {
        Collider2D collider = this.GetComponent<Collider2D>();

        collider.offset = this.position;
    }
}
