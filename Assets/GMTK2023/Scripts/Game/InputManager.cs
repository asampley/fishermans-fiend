using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 mouseDown;

    public static event Action<MouseDragEvent> MouseDrag;

    void OnEnable() {
        MouseDrag += PrintEvent;
    }

    void OnDisable() {
        MouseDrag -= PrintEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseDrag?.Invoke
            (
                new((Vector2)mouseDown,
                (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition))
            );
        }
    }

    public class MouseDragEvent
    {
        public Vector2 mouseDown;
        public Vector2 mouseUp;

        public MouseDragEvent(Vector2 mouseDown, Vector2 mouseUp)
        {
            this.mouseDown = mouseDown;
            this.mouseUp = mouseUp;
        }

        public override string ToString()
        {
            return "{ mouseDown = " + mouseDown + ", mouseUp = " + mouseUp + "}";
        }
    }

    private static void PrintEvent(MouseDragEvent ev)
    {
        Debug.Log("Drag event: " + ev);
    }
}
