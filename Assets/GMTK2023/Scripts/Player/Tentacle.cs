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
        pull.enabled = true;
        pull.target = obj;

        var body = this.GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        body.velocity = Vector2.zero;
    }
}
