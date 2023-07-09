using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 mouseDown;

    public static event Action<AttackEvent> Attack;
    public static event Action<InkCloudEvent> InkCloud;
    public static event Action<SirenSongEvent> SirenSong;
    public static event Action<MouseDragEvent> MouseDrag;
    public static event Action<MouseDragInProgressEvent> MouseDragInProgress;

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

        if (Input.GetMouseButton(0))
        {
            MouseDragInProgressEvent ev = new(
                mouseDown,
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
            );

            MouseDragInProgress?.Invoke(ev);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseDrag?.Invoke
            (
                new((Vector2)mouseDown,
                (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition))
            );
        }

        if (Input.GetMouseButtonDown(1))
        {
            Attack?.Invoke(new(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            InkCloud?.Invoke(null);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SirenSong?.Invoke(null);
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

    public class MouseDragInProgressEvent
    {
        public Vector2 mouseDown;
        public Vector2 mouseAt;

        public MouseDragInProgressEvent(Vector2 mouseDown, Vector2 mouseAt)
        {
            this.mouseDown = mouseDown;
            this.mouseAt = mouseAt;
        }

        public override string ToString()
        {
            return "{ mouseDown = " + mouseDown + ", mouseAt = " + mouseAt + "}";
        }
    }

    public class InkCloudEvent
    {

    }

    public class SirenSongEvent
    {

    }

    public class AttackEvent
    {
        public Vector2 target;

        public AttackEvent(Vector2 targetPos)
        {
            target = targetPos;
        }
    }

    private static void PrintEvent(MouseDragEvent ev)
    {
        Debug.Log("Drag event: " + ev);
    }
}
