using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public void Grab(GameObject obj)
    {
        this.GetComponent<TentacleLaunch>().enabled = false;

        var pull = this.GetComponent<TentaclePull>();
        pull.target = obj;
        pull.enabled = true;

        var body = this.GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        body.velocity = Vector2.zero;
    }

    public void Fall()
    {
        this.GetComponent<TentaclePull>().enabled = false;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
