using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleLaunch : MonoBehaviour
{
    [SerializeField]
    private Vector2 position = Vector2.zero;

    private Vector3[] positionBuffer = new Vector3[1];

    public Vector2 velocity = Vector2.zero;
    public Vector2 acceleration = Vector2.down;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.velocity += this.acceleration * Time.fixedDeltaTime;
        this.position += this.velocity * Time.fixedDeltaTime;

        TentacleDespawn despawn = this.GetComponent<TentacleDespawn>();
        despawn.peakY = Math.Max(despawn.peakY, this.position.y);

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.enabled) {
            collision.GetComponent<ICollidable>().Collide(this.GetComponent<Tentacle>());
        }
    }

    public void Grab(ICollidable obj)
    {
        this.enabled = false;
        this.GetComponent<TentaclePull>().enabled = true;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
